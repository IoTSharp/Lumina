using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
#if REFERENCE
using System.Windows.Forms;
#else
using Lumina.Forms;
#endif

// This is the Parent/ControlCollection/Form-chain slice of Control-V. It is not
// a whole-Control acceptance suite: lazy Handle, input, layout and designer
// round-trips remain separate. All HWNDs below come from actual shown windows.
internal static class Program
{
    private static readonly List<string> Events = [];
    private static readonly List<Control> IsolatedManagedFixtures = [];
    private static readonly InvalidOperationException Failure = new("collection observer failure");

    [STAThread]
    private static void Main()
    {
        Console.OutputEncoding = new UTF8Encoding(false);
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
#if REFERENCE
        string? version = typeof(Control).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        if (version is null || !version.StartsWith("10.0.11+", StringComparison.Ordinal))
            throw new InvalidOperationException($"Expected WinForms 10.0.11, found {version}.");
#endif
        WindowsFormsSynchronizationContext.AutoInstall = false;
        SynchronizationContext.SetSynchronizationContext(null);
        RunGroup(nameof(Collections), Collections);
        RunGroup(nameof(LookupAndEnumeration), LookupAndEnumeration);
        RunGroup(nameof(FindGetterExceptions), FindGetterExceptions);
        RunGroup(nameof(FactoryAndConstructors), FactoryAndConstructors);
        RunGroup(nameof(NotificationFailures), NotificationFailures);
        RunGroup(nameof(FormChain), FormChain);
        RunGroup(nameof(ActiveControls), ActiveControls);
        RunGroup(nameof(ActiveControlTransitions), ActiveControlTransitions);
        RunGroup(nameof(ActiveControlRemovalCandidates), ActiveControlRemovalCandidates);
        RunGroup(nameof(ContainerDisposalState), ContainerDisposalState);
        RunGroup(nameof(ManagedDisposal), ManagedDisposal);
        RunGroup(nameof(DisposalParentNotificationFailure), DisposalParentNotificationFailure);
        RunGroup(nameof(CrossThread), CrossThread);
        RunGroup(nameof(NativeLifetime), NativeLifetime);
        RunGroup(nameof(FormDisposalState), FormDisposalState);
        Record("complete", true);
    }

    private static void Collections()
    {
        using var a = Traced("a");
        using var b = Traced("b");
        using var x = Traced("x");
        using var y = Traced("y");
        using var z = Traced("z");
        var controls = a.Controls;
        IList list = controls;
        ICollection collection = controls;
        Record("interfaces", $"list:{!ReferenceEquals(list, null)}|collection:{!ReferenceEquals(collection, null)}|enumerable:{controls is IEnumerable}|cloneable:{controls is ICloneable}");
        Record("collection-flags", $"count:{controls.Count}/{list.Count}/{collection.Count}|readonly:{controls.IsReadOnly}/{list.IsReadOnly}|fixed:{list.IsFixedSize}|sync:{collection.IsSynchronized}|root-self:{ReferenceEquals(collection.SyncRoot, controls)}|owner:{Id(controls.Owner)}|cached:{ReferenceEquals(controls, a.Controls)}");
        Observe("add-null", () => controls.Add(value: null!), a, b, x);
        Observe("add-x", () => controls.Add(value: x), a, b, x);
        Observe("add-y", () => controls.Add(value: y), a, b, x, y);
        Observe("add-duplicate", () => controls.Add(value: x), a, b, x, y);
        Observe("parent-same", () => x.Parent = a, a, b, x, y);
        Observe("parent-move", () => x.Parent = b, a, b, x, y);
        Observe("add-move", () => controls.Add(value: x), a, b, x, y);
        Observe("parent-null", () => x.Parent = null, a, b, x, y);
        Observe("remove-missing", () => controls.Remove(value: x), a, b, x, y);
        Observe("remove-null", () => controls.Remove(value: null!), a, b, x, y);
        Observe("self-cycle", () => a.Parent = a, a, b, x, y);
        Observe("ancestor-cycle", () => y.Controls.Add(value: a), a, b, x, y);
        Observe("range-null", () => controls.AddRange(controls: null!), a, b, x, y);
        Observe("range-null-duplicate", () => controls.AddRange(controls: [x, null!, x, z]), a, b, x, y, z);
        Observe("range-partial-cycle", () => b.Controls.AddRange(controls: [x, b, z]), a, b, x, y, z);
        Observe("list-add-null", () => Record("list-add-null-return", list.Add(value: null)), a, b, x);
        Observe("list-add-wrong", () => Record("list-add-wrong-return", list.Add(value: "wrong")), a, b, x);
        Observe("list-add-move", () => Record("list-add-return", list.Add(value: x)), a, b, x);
        Observe("list-add-duplicate", () => Record("list-duplicate-return", list.Add(value: x)), a, b, x);
        Record("queries", $"contains:{controls.Contains(control: x)}/{list.Contains(value: x)}|null:{controls.Contains(control: null!)}/{list.Contains(value: null)}|wrong:{list.Contains(value: "wrong")}|indices:{controls.IndexOf(control: x)}/{list.IndexOf(value: x)}/{list.IndexOf(value: "wrong")}");
        foreach (int index in new[] { -1, 0, controls.Count, int.MaxValue })
        {
            Observe("typed-index-" + index, () => Record("index-value", Id(controls[index])), a);
            Observe("list-index-" + index, () => Record("list-index-value", Id((Control?)list[index])), a);
            Observe("list-set-" + index, () => list[index] = x, a, x);
            Observe("list-set-wrong-" + index, () => list[index] = "wrong", a, x);
            Observe("list-insert-" + index, () => list.Insert(index: index, value: x), a, x);
            Observe("list-insert-wrong-" + index, () => list.Insert(index: index, value: "wrong"), a, x);
        }
        Observe("list-remove-wrong", () => list.Remove(value: "wrong"), a, x);
        Observe("list-remove-null", () => list.Remove(value: null), a, x);
        Observe("list-remove-x", () => list.Remove(value: x), a, x);
        Observe("list-removeat-invalid", () => list.RemoveAt(index: -1), a);
        Observe("typed-removeat-invalid", () => controls.RemoveAt(index: controls.Count), a);
        controls.Add(x);
        Observe("list-removeat", () => list.RemoveAt(index: controls.Count - 1), a, x);
        Observe("typed-removeat", () => controls.RemoveAt(index: 0), a, y, z);
        controls.AddRange([x, y, z]);
        Observe("list-clear", list.Clear, a, x, y, z);
        controls.AddRange([x, y]);
        Observe("typed-clear", controls.Clear, a, x, y, z);
        Observe("clear-empty", controls.Clear, a);
        Observe("collection-null-owner", () => Record("null-owner", Id(new Control.ControlCollection(owner: null!).Owner)));
    }

    private static void LookupAndEnumeration()
    {
        using var owner = new Control { Name = "owner" };
        using var first = new Control { Name = "KEY" };
        using var second = new Control { Name = "key" };
        using var branch = new Control { Name = "branch" };
        using var nested = new Control { Name = "Key" };
        using var missing = new Control { Name = "missing" };
        owner.Controls.AddRange([first, branch, second]);
        branch.Controls.Add(nested);
        foreach (string? key in new string?[] { null, "", "key", "KEY", "missing", "中文" })
        {
            string label = key is null ? "null" : key.Length == 0 ? "empty" : key;
            Observe("lookup-" + label, () => Record("lookup", $"item:{Id(owner.Controls[key!])}|contains:{owner.Controls.ContainsKey(key: key!)}|index:{owner.Controls.IndexOfKey(key: key!)}"));
            foreach (bool recurse in new[] { false, true })
                Observe($"find-{label}-{recurse}", () => Record("find", Names(owner.Controls.Find(key: key!, searchAllChildren: recurse))));
        }
        Record("contains-tree", $"self:{owner.Contains(owner)}|child:{owner.Contains(first)}|nested:{owner.Contains(nested)}|null:{owner.Contains(null!)}|collection-nested:{owner.Controls.Contains(nested)}");
        owner.Controls.IndexOfKey("key");
        first.Name = "renamed";
        Record("lookup-rename", Id(owner.Controls["key"]));
        first.Name = "key";
        Record("lookup-cache", Id(owner.Controls["key"]) + ":" + owner.Controls.IndexOfKey("key"));
        foreach (Control? child in new Control?[] { first, missing, null })
        foreach (bool throwing in new[] { false, true })
            Observe($"child-index-{Id(child)}-{throwing}", () => Record("child-index", owner.Controls.GetChildIndex(child: child!, throwException: throwing)));
        Observe("child-index-overload", () => Record("child-index", owner.Controls.GetChildIndex(child: first)));
        foreach (int index in new[] { 2, 0, -1, 99, int.MinValue })
            Observe("set-child-index-" + index, () => owner.Controls.SetChildIndex(child: first, newIndex: index), owner);
        Observe("set-missing-index", () => owner.Controls.SetChildIndex(child: missing, newIndex: -1), owner);
        Observe("set-null-index", () => owner.Controls.SetChildIndex(child: null!, newIndex: -1), owner);
        foreach (bool throughInterface in new[] { false, true })
        {
            Array?[] arrays = [new Control[6], new object[6], new string[6], new Control[1], new Control[2, 2], null,
                new int[6], new IDisposable[6], new IWin32Window[6]];
            for (int i = 0; i < arrays.Length; i++)
            {
                Array array;
                if (i == 5)
                {
                    if (!TryCreateBoundArray(1, $"copy-{throughInterface}-{i}", "copy-content", out Array? created)) continue;
                    array = created;
                }
                else array = arrays[i]!;
                Observe($"copy-{throughInterface}-{i}", () =>
                {
                    if (throughInterface) ((ICollection)owner.Controls).CopyTo(array: array, index: 1);
                    else owner.Controls.CopyTo(array: array, index: 1);
                });
                Record("copy-content", string.Join(",", array.Cast<object?>().Select(value => value is Control control ? Id(control) : value?.ToString() ?? "null")));
            }
            Observe("copy-null-" + throughInterface, () =>
            {
                if (throughInterface) ((ICollection)owner.Controls).CopyTo(array: null!, index: -1);
                else owner.Controls.CopyTo(array: null!, index: -1);
            });
            Observe("copy-negative-" + throughInterface, () => ((ICollection)owner.Controls).CopyTo(array: new Control[6], index: -1));
            foreach ((int lowerBound, int index) in new[] { (1, -1), (1, 0), (1, 1), (-2, -3), (-2, -2), (-2, -1), (-2, 1) })
            {
                if (!TryCreateBoundArray(lowerBound, $"copy-lower-bound-{throughInterface}-{lowerBound}-{index}", "copy-lower-bound-content", out Array? array)) continue;
                Observe($"copy-lower-bound-{throughInterface}-{lowerBound}-{index}", () =>
                {
                    if (throughInterface) ((ICollection)owner.Controls).CopyTo(array: array, index: index);
                    else owner.Controls.CopyTo(array: array, index: index);
                });
                Record("copy-lower-bound-content", string.Join(",", array.Cast<Control?>().Select(Id)));
            }
            using var emptyOwner = new Control { Name = "empty-copy-owner" };
            foreach (int index in new[] { 0, -1 })
            foreach (string type in new[] { "string", "int" })
            {
                Array array = type == "string" ? new string[2] : new int[2];
                Observe($"copy-empty-{throughInterface}-{type}-{index}", () =>
                {
                    if (throughInterface) ((ICollection)emptyOwner.Controls).CopyTo(array: array, index: index);
                    else emptyOwner.Controls.CopyTo(array: array, index: index);
                });
                Record("copy-empty-content", string.Join(",", array.Cast<object?>().Select(value => value?.ToString() ?? "null")));
            }
        }
        RunGroup(nameof(CloneMutation), CloneMutation);
        owner.Controls.Add(first);
        Observe("removebykey-null", () => owner.Controls.RemoveByKey(key: null!), owner);
        Observe("removebykey-empty", () => owner.Controls.RemoveByKey(key: ""), owner);
        Observe("removebykey", () => owner.Controls.RemoveByKey(key: "KEY"), owner, first, second);
        owner.Controls.Add(first);
        foreach (string mutation in new[] { "none", "add", "remove", "clear", "reorder" })
        {
            Console.Error.WriteLine("CHECKPOINT Enumeration:" + mutation);
            owner.Controls.Clear();
            owner.Controls.AddRange([first, branch, second]);
            IEnumerator enumerator = mutation == "none" ? owner.Controls.GetEnumerator() : ((IEnumerable)owner.Controls).GetEnumerator();
            Observe("enumerator-current-before-" + mutation, () => Record("current", Id((Control?)enumerator.Current)));
            Record("enumerator-first-" + mutation, enumerator.MoveNext() + ":" + Id((Control?)enumerator.Current));
            switch (mutation)
            {
                case "add": owner.Controls.Add(missing); break;
                case "remove": owner.Controls.Remove(first); break;
                case "clear": owner.Controls.Clear(); break;
                case "reorder": owner.Controls.SetChildIndex(second, 0); break;
            }
            Observe("enumerator-rest-" + mutation, () =>
            {
                var rest = new List<string>();
                while (enumerator.MoveNext()) rest.Add(Id((Control?)enumerator.Current));
                Record("rest", string.Join(",", rest));
            });
            Observe("enumerator-current-after-" + mutation, () => Record("current", Id((Control?)enumerator.Current)));
            Observe("enumerator-reset-" + mutation, () => { enumerator.Reset(); Record("reset-first", enumerator.MoveNext()); });
        }
    }

    private static bool TryCreateBoundArray(int lowerBound, string observationName, string contentName, [NotNullWhen(true)] out Array? array)
    {
        try
        {
            array = Array.CreateInstance(typeof(Control), [6], [lowerBound]);
            return true;
        }
        catch (PlatformNotSupportedException exception)
        {
            // Native AOT can reject this fixture before any ControlCollection
            // method runs. Keep that platform limitation as a raw difference;
            // it is neither a CopyTo exception nor a successful CopyTo test.
            array = null;
            Record(observationName, "fixture-creation-exception:" + exception.GetType().Name + "|copy:not-run");
            Record(contentName, "not-created");
            Console.Error.WriteLine($"FIXTURE LIMITATION {observationName}: {exception}");
            return false;
        }
    }

    private static void CloneMutation()
    {
        // A clone shares Owner and child references but has its own list. Removing
        // a cloned child clears Parent while the original list still contains it.
        // Keep this reference behavior observable without feeding that inconsistent
        // list into later Clear/Dispose loops. These fixtures never acquire HWNDs
        // and stay rooted until process exit; normal fixtures retain using cleanup.
        var owner = new Control { Name = "owner" };
        var first = new Control { Name = "key" };
        var second = new Control { Name = "key" };
        var branch = new Control { Name = "branch" };
        owner.Controls.AddRange([branch, second, first]);
        IsolatedManagedFixtures.AddRange([owner, first, second, branch]);
        var clone = (Control.ControlCollection)((ICloneable)owner.Controls).Clone();
        Record("clone", $"same:{ReferenceEquals(clone, owner.Controls)}|owner:{Id(clone.Owner)}|items:{Names(clone)}|parents:{string.Join(",", clone.Cast<Control>().Select(c => Id(c.Parent)))}");
        Record("collection-equality", $"self:{owner.Controls.Equals(owner.Controls)}|clone:{owner.Controls.Equals(clone)}|null:{owner.Controls.Equals(null)}|hash-stable:{owner.Controls!.GetHashCode() == owner.Controls.GetHashCode()}");
        Observe("clone-remove", () => clone.Remove(first), owner, first);
        Record("clone-after-remove", Names(clone));
        if (IsolatedManagedFixtures.Any(control => control.IsHandleCreated))
            throw new InvalidOperationException("The isolated clone fixture must remain entirely managed.");
    }

    private static void FindGetterExceptions()
    {
        foreach (bool critical in new[] { false, true })
        {
            using var owner = new ThrowingFindControl { Name = "find-owner" };
            using var first = new Control { Name = "match" };
            using var second = new Control { Name = "other" };
            using var third = new Control { Name = "match" };
            owner.Controls.AddRange([first, second, third]);
            Exception failure = critical ? new IndexOutOfRangeException("critical find getter") : Failure;
            owner.GetterFailure = failure;
            try
            {
                Observe("find-indexer-failure-" + critical, () =>
                {
                    try
                    {
                        Control[] found = owner.Controls.Find(key: "match", searchAllChildren: false);
                        Record("find-indexer-result", $"count:{found.Length}|first-is-prefix:{found.Length > 0 && ReferenceEquals(found[0], first)}|items:{Names(found)}");
                    }
                    catch (Exception exception)
                    {
                        Record("find-indexer-original-exception", ReferenceEquals(exception, failure));
                        throw;
                    }
                });
            }
            finally { owner.GetterFailure = null; }
        }
    }

    private static void FactoryAndConstructors()
    {
        using var owner = new FactoryControl { Name = "factory" };
        Record("factory-before", owner.FactoryCalls);
        Control.ControlCollection collection = owner.Controls;
        Record("factory-created", $"calls:{owner.FactoryCalls}|derived:{collection is DerivedCollection}|cached:{ReferenceEquals(collection, owner.Controls)}|owner:{Id(collection.Owner)}");
        using var child = new Control { Name = "child" };
        Observe("factory-parent", () => child.Parent = owner, owner, child);
        Observe("factory-range", () => collection.AddRange(controls: [child]), owner, child);
        var cloned = (Control.ControlCollection)((ICloneable)collection).Clone();
        Record("factory-clone", $"calls:{owner.FactoryCalls}|derived:{cloned is DerivedCollection}|owner:{Id(cloned.Owner)}|items:{Names(cloned)}|parent:{Id(child.Parent)}");
        Observe("factory-parent-null", () => child.Parent = null, owner, child);
        using var parent = Traced("constructor-parent");
        foreach (bool nullParent in new[] { false, true })
        foreach (bool bounds in new[] { false, true })
        {
            Events.Clear();
            using var created = bounds
                ? new ConstructorControl(nullParent ? null : parent, "text", 7, 9, 31, 37)
                : new ConstructorControl(nullParent ? null : parent, null);
            Record($"constructor-{nullParent}-{bounds}", $"parent:{Id(created.Parent)}|text:{created.Text}|bounds:{Rect(created.Bounds)}|calls:{string.Join(";", Events)}");
        }
        var manual = new Control.ControlCollection(owner: owner).Owner;
        Record("manual-collection-owner", ReferenceEquals(manual, owner));
    }

    private static void NotificationFailures()
    {
        foreach (string point in new[] { "parent", "added", "removed" })
        {
            using var a = Traced("a");
            using var b = Traced("b");
            using var child = Traced("child");
            a.Controls.Add(child);
            EventHandler parentFailure = (_, _) => throw Failure;
            ControlEventHandler collectionFailure = (_, _) => throw Failure;
            if (point == "parent") child.ParentChanged += parentFailure;
            if (point == "added") b.ControlAdded += collectionFailure;
            if (point == "removed") a.ControlRemoved += collectionFailure;
            Observe("move-failure-" + point, () => child.Parent = b, a, b, child);
            child.ParentChanged -= parentFailure;
            b.ControlAdded -= collectionFailure;
            a.ControlRemoved -= collectionFailure;
        }
        using var reentrantOwner = Traced("reentrant");
        using var reentrantChild = Traced("child");
        bool entered = false;
        reentrantOwner.ControlAdded += (_, e) => { if (!entered) { entered = true; reentrantOwner.Controls.Remove(e.Control!); } };
        Observe("added-removes-child", () => reentrantOwner.Controls.Add(reentrantChild), reentrantOwner, reentrantChild);
    }

    private static void FormChain()
    {
        using var form = new Form { Name = "form" };
        Control control = form;
        ScrollableControl scrollable = form;
        ContainerControl container = form;
        Record("form-chain", $"control:{ReferenceEquals(control, form)}|scrollable:{ReferenceEquals(scrollable, form)}|container:{ReferenceEquals(container, form)}|component:{form is System.ComponentModel.Component}|window:{form is IWin32Window}|container-interface:{form is IContainerControl}");
        Record("form-default", $"size:{form.Size.Width},{form.Size.Height}|client:{form.ClientSize.Width},{form.ClientSize.Height}|visible:{form.Visible}/{control.Visible}|tabstop:{form.TabStop}/{control.TabStop}|text:{form.Text}|handle:{control.IsHandleCreated}");
        Record("form-collections", ReferenceEquals(form.Controls, control.Controls) && ReferenceEquals(container.Controls, control.Controls));
        using var child = Traced("form-child");
        Observe("form-add", () => control.Controls.Add(child), form, child);
        Record("form-child-parent", ReferenceEquals(child.Parent, form));
        object tag = new();
        control.Tag = tag;
        control.Text = "shared text";
        control.Bounds = new Rectangle(13, 17, 340, 260);
        control.Enabled = false;
        Record("form-shared-state", $"tag:{ReferenceEquals(form.Tag, tag)}|text:{form.Text == control.Text}|bounds:{form.Bounds == control.Bounds}|enabled:{form.Enabled}/{control.Enabled}");
        form.Text = "form writes";
        form.Location = new Point(19, 23);
        form.Enabled = true;
        Record("form-shared-reverse", $"text:{control.Text}|location:{control.Left},{control.Top}|enabled:{control.Enabled}");
        Observe("form-reject-top-level", () => child.Controls.Add(form), form, child);
    }

    private static void ActiveControls()
    {
        using var container = new ContainerControl { Name = "container" };
        using var child = new Control { Name = "child" };
        using var nested = new ContainerControl { Name = "nested" };
        using var leaf = new Control { Name = "leaf" };
        using var outside = new Control { Name = "outside" };
        container.Controls.AddRange([child, nested]);
        nested.Controls.Add(leaf);
        IContainerControl contract = container;
        foreach (Control? value in new Control?[] { null, outside, container, child, leaf, nested, null })
        {
            Observe("active-set-" + Id(value), () => contract.ActiveControl = value);
            Record("active-state", Id(container.ActiveControl) + "/" + Id(nested.ActiveControl));
            Observe("activate-" + Id(value), () => Record("activate-return", contract.ActivateControl(active: value!)));
            Record("activate-state", Id(contract.ActiveControl) + "/" + Id(nested.ActiveControl));
        }
        container.ActiveControl = child;
        container.Controls.Remove(child);
        Record("active-after-remove", Id(container.ActiveControl));
        container.Controls.Add(child);
        container.ActiveControl = child;
        child.Dispose();
        Record("active-after-dispose", Id(container.ActiveControl));
    }

    private static void ActiveControlTransitions()
    {
        foreach (string operation in new[] { "set-child", "set-null", "activate-null", "activate-outside", "activate-self" })
        {
            Console.Error.WriteLine("CHECKPOINT ActiveControlTransitions:" + operation);
            using var container = new ContainerControl { Name = "container" };
            using var child = new Control { Name = "child" };
            using var nested = new ContainerControl { Name = "nested" };
            using var leaf = new Control { Name = "leaf" };
            using var outside = new Control { Name = "outside" };
            container.Controls.AddRange([child, nested]);
            nested.Controls.Add(leaf);
            container.ActiveControl = leaf;
            Record("active-transition-before-" + operation, Id(container.ActiveControl) + "/" + Id(nested.ActiveControl));
            IContainerControl contract = container;
            Observe("active-transition-" + operation, () =>
            {
                switch (operation)
                {
                    case "set-child": container.ActiveControl = child; break;
                    case "set-null": container.ActiveControl = null; break;
                    case "activate-null": Record("active-transition-return", contract.ActivateControl(null!)); break;
                    case "activate-outside": Record("active-transition-return", contract.ActivateControl(outside)); break;
                    case "activate-self": Record("active-transition-return", contract.ActivateControl(container)); break;
                }
            });
            Record("active-transition-after-" + operation, $"active:{Id(container.ActiveControl)}/{Id(nested.ActiveControl)}|handles:{container.IsHandleCreated}/{nested.IsHandleCreated}/{leaf.IsHandleCreated}");
        }
    }

    private static void ActiveControlRemovalCandidates()
    {
        foreach (string mode in new[] { "order", "tab-order", "first-no-tabstop", "first-hidden", "first-disabled", "none-selectable", "plain-with-leaf", "container-with-leaf" })
        {
            Console.Error.WriteLine("CHECKPOINT ActiveControlRemovalCandidates:" + mode);
            using var owner = new ContainerControl { Name = "owner" };
            using var active = new Control { Name = "active", TabIndex = 0 };
            using Control first = mode == "container-with-leaf" ? new ContainerControl { Name = "first", TabIndex = 1 } : new Control { Name = "first", TabIndex = 1 };
            using var second = new Control { Name = "second", TabIndex = 2 };
            using var third = new Control { Name = "third", TabIndex = 3 };
            using var leaf = new Control { Name = "leaf", TabIndex = 0 };
            owner.Controls.AddRange([active, first, second, third]);
            switch (mode)
            {
                case "tab-order": first.TabIndex = 30; second.TabIndex = 10; third.TabIndex = 20; break;
                case "first-no-tabstop": first.TabStop = false; break;
                case "first-hidden": first.Visible = false; break;
                case "first-disabled": first.Enabled = false; break;
                case "none-selectable": first.TabStop = false; second.Visible = false; third.Enabled = false; break;
                case "plain-with-leaf":
                case "container-with-leaf": first.TabStop = false; first.Controls.Add(leaf); break;
            }
            owner.ActiveControl = active;
            Observe("active-remove-candidate-" + mode, () => owner.Controls.Remove(active));
            Record("active-remove-result-" + mode, $"active:{Id(owner.ActiveControl)}|nested:{(first is ContainerControl nested ? Id(nested.ActiveControl) : "not-container")}|leaf-parent:{Id(leaf.Parent)}|handles:{owner.IsHandleCreated}/{first.IsHandleCreated}/{leaf.IsHandleCreated}");
        }
    }

    private static void ContainerDisposalState()
    {
        using var owner = new ContainerControl { Name = "dispose-container" };
        using var nested = new ContainerControl { Name = "dispose-nested" };
        using var leaf = new Control { Name = "dispose-leaf" };
        owner.Controls.Add(nested);
        nested.Controls.Add(leaf);
        owner.ActiveControl = leaf;
        Record("container-dispose-before", $"active:{Id(owner.ActiveControl)}/{Id(nested.ActiveControl)}|visible:{owner.Visible}/{nested.Visible}/{leaf.Visible}");
        owner.Dispose();
        Record("container-dispose-after", $"active:{Id(owner.ActiveControl)}/{Id(nested.ActiveControl)}|visible:{owner.Visible}/{nested.Visible}/{leaf.Visible}|disposed:{owner.IsDisposed}/{nested.IsDisposed}/{leaf.IsDisposed}|handles:{owner.IsHandleCreated}/{nested.IsHandleCreated}/{leaf.IsHandleCreated}");
    }

    private static void FormDisposalState()
    {
        // Run after the message-loop scenario so this direct Show/Close scope
        // cannot leave quit messages ahead of an unrelated Application.Run.
        foreach (bool shown in new[] { false, true })
        foreach (bool close in new[] { false, true })
        {
            string name = $"form-disposal-{shown}-{(close ? "close" : "dispose")}";
            Console.Error.WriteLine("CHECKPOINT " + name);
            using var form = new Form { Name = "dispose-form", Text = "Active control disposal acceptance", Size = new Size(200, 140) };
            using var child = new Control { Name = "dispose-child", Bounds = new Rectangle(5, 5, 30, 20) };
            form.Controls.Add(child);
            if (shown) form.Show();
            form.ActiveControl = child;
            void DisposalEvent(string kind) => Events.Add($"{kind}:visible={form.Visible}/{child.Visible}:disposed={form.IsDisposed}/{child.IsDisposed}:disposing={form.Disposing}/{child.Disposing}:active={Id(form.ActiveControl)}");
            form.VisibleChanged += (_, _) => DisposalEvent("form-visible");
            child.VisibleChanged += (_, _) => DisposalEvent("child-visible");
            form.Disposed += (_, _) => DisposalEvent("form-disposed");
            child.Disposed += (_, _) => DisposalEvent("child-disposed");
            nint handle = form.IsHandleCreated ? form.Handle : 0;
            Record(name + "-before", $"active:{Id(form.ActiveControl)}|visible:{form.Visible}/{((Control)form).Visible}/{child.Visible}|handles:{form.IsHandleCreated}/{child.IsHandleCreated}|native:{handle != 0 && IsWindow(handle)}");
            Observe(name, () => { if (close) form.Close(); else form.Dispose(); });
            Record(name + "-after", $"active:{Id(form.ActiveControl)}|visible:{form.Visible}/{((Control)form).Visible}/{child.Visible}|disposed:{form.IsDisposed}/{child.IsDisposed}|handles:{form.IsHandleCreated}/{child.IsHandleCreated}|native-alive:{handle != 0 && IsWindow(handle)}");
        }
    }

    private static void ManagedDisposal()
    {
        using var owner = Traced("owner");
        using var child = Traced("child");
        using var grandchild = Traced("grandchild");
        owner.Controls.Add(child);
        child.Controls.Add(grandchild);
        foreach (Control control in new[] { owner, child, grandchild })
            control.Disposed += (_, _) => Events.Add($"disposed:{Id(control)}:parent:{Id(control.Parent)}:disposing:{control.Disposing}:disposed:{control.IsDisposed}");
        Observe("dispose-tree", owner.Dispose, owner, child, grandchild);
        Observe("dispose-tree-repeat", owner.Dispose, owner, child, grandchild);
        using var survivingOwner = Traced("surviving-owner");
        using var disposedChild = Traced("disposed-child");
        disposedChild.Dispose();
        Observe("add-disposed", () => survivingOwner.Controls.Add(disposedChild), survivingOwner, disposedChild);
        Observe("dispose-reattached-child", disposedChild.Dispose, survivingOwner, disposedChild);
        using var disposedOwner = Traced("disposed-owner");
        disposedOwner.Dispose();
        using var newChild = Traced("new-child");
        Observe("add-to-disposed-owner", () => disposedOwner.Controls.Add(newChild), disposedOwner, newChild);
        Observe("dispose-owner-with-new-child", disposedOwner.Dispose, disposedOwner, newChild);
    }

    private static void DisposalParentNotificationFailure()
    {
        using var components = new System.ComponentModel.Container();
        using var owner = new Control { Name = "site-owner" };
        using var child = new Control { Name = "site-child" };
        owner.Controls.Add(child);
        components.Add(child, "child-site");
        int disposedEvents = 0;
        child.Disposed += (_, _) => disposedEvents++;
        EventHandler parentFailure = (_, _) =>
        {
            Events.Add($"parent-failure:disposed={child.IsDisposed}:disposing={child.Disposing}:parent={Id(child.Parent)}:site={child.Site is not null}:components={components.Components.Count}");
            throw Failure;
        };
        child.ParentChanged += parentFailure;
        try
        {
            Observe("dispose-parent-notification-failure", child.Dispose, owner, child);
            Record("dispose-parent-notification-site", $"has-site:{child.Site is not null}|same-container:{ReferenceEquals(child.Site?.Container, components)}|components:{components.Components.Count}|disposed-events:{disposedEvents}");
        }
        finally
        {
            child.ParentChanged -= parentFailure;
            child.Dispose();
        }
    }

    private static void CrossThread()
    {
        using var owner = new Control { Name = "thread-owner" };
        Control? foreign = null;
        var constructionThread = new Thread(() => foreign = new Control { Name = "foreign" });
        constructionThread.SetApartmentState(ApartmentState.STA);
        constructionThread.Start();
        if (!constructionThread.Join(10000)) throw new TimeoutException("Foreign constructor thread did not exit.");
        using (foreign)
            Observe("cross-thread-without-handles", () => owner.Controls.Add(foreign!), owner, foreign!);
    }

    private static void NativeLifetime()
    {
        using var form = new Form { Name = "native-form", Text = "Control parent collection acceptance", Bounds = new Rectangle(20, 20, 400, 260) };
        using var other = new Form { Name = "native-other", Text = "Control reparent destination", Bounds = new Rectangle(430, 20, 400, 260) };
        using var a = new Control { Name = "native-a", Bounds = new Rectangle(10, 10, 120, 120) };
        using var b = new Control { Name = "native-b", Bounds = new Rectangle(140, 10, 120, 120) };
        using var x = new Control { Name = "native-x", Bounds = new Rectangle(5, 5, 30, 20) };
        using var y = new Control { Name = "native-y", Bounds = new Rectangle(10, 30, 30, 20) };
        using var z = new Control { Name = "native-z", Bounds = new Rectangle(15, 55, 30, 20) };
        form.Controls.AddRange([a, b]);
        a.Controls.AddRange([x, y, z]);
        Exception? failure = null;
        nint formHandle = 0;
        nint[] originalHandles = [];
        int closing = 0;
        int disposed = 0;
        form.FormClosing += (_, _) => closing++;
        ((Control)form).Disposed += (_, _) => disposed++;
        form.Shown += (_, _) => form.BeginInvoke((Action)(() =>
        {
            try
            {
                other.Show();
                formHandle = form.Handle;
                originalHandles = [a.Handle, b.Handle, x.Handle, y.Handle, z.Handle];
                if (!IsWindow(formHandle) || originalHandles.Any(handle => !IsWindow(handle)))
                    throw new InvalidOperationException("The native scenario requires real shown HWNDs for every control.");
                Record("native-form-shared-handle", ((Control)form).Handle == formHandle && ((IWin32Window)form).Handle == formHandle);
                Record("native-direct-parent", GetParent(a.Handle) == formHandle && ReferenceEquals(a.Parent, form));
                NativeSnapshot("native-initial", a, x, y, z);
                a.Controls.SetChildIndex(z, 0);
                NativeSnapshot("native-reorder-first", a, x, y, z);
                a.Controls.SetChildIndex(z, -1);
                NativeSnapshot("native-reorder-last", a, x, y, z);
                nint xHandle = x.Handle;
                x.Parent = b;
                Record("native-move-sibling", $"parent:{GetParent(x.Handle) == b.Handle}|managed:{ReferenceEquals(x.Parent, b)}|same-handle:{x.Handle == xHandle}|valid:{IsWindow(xHandle)}");
                x.Parent = other;
                Record("native-move-form", $"parent:{GetParent(x.Handle) == other.Handle}|managed:{ReferenceEquals(x.Parent, other)}|same-handle:{x.Handle == xHandle}");
                other.Controls.Remove(x);
                Record("native-remove", $"parent-null:{x.Parent is null}|disposed:{x.IsDisposed}|same-handle:{x.Handle == xHandle}|valid:{IsWindow(xHandle)}|not-old-parent:{GetParent(xHandle) != other.Handle}");
                a.Controls.Add(x);
                Record("native-reattach", $"parent:{GetParent(x.Handle) == a.Handle}|same-handle:{x.Handle == xHandle}");
                RunGroup(nameof(NativeHiddenParentReparent), () => NativeHiddenParentReparent(form, other));
                RunGroup(nameof(NativeCrossThread), () => NativeCrossThread(form));
                nint yHandle = y.Handle;
                y.Dispose();
                Record("native-dispose-child", $"invalid:{!IsWindow(yHandle)}|removed:{!a.Controls.Contains(y)}|parent-null:{y.Parent is null}|disposed:{y.IsDisposed}");
            }
            catch (Exception exception) { failure = exception; }
            finally { other.Close(); form.Close(); }
        }));
        Application.Run(form);
        if (failure is not null) throw new InvalidOperationException("Native parent collection scenario failed.", failure);
        Record("native-normal-close", $"closing:{closing}|disposed-event:{disposed}|disposed:{form.IsDisposed}|form-invalid:{!IsWindow(formHandle)}|children-invalid:{originalHandles.All(handle => !IsWindow(handle))}");
    }

    private static void NativeHiddenParentReparent(Form firstForm, Form secondForm)
    {
        using var source = new Control { Name = "hidden-source", Bounds = new Rectangle(5, 145, 90, 50) };
        using var destination = new Control { Name = "visible-destination", Bounds = new Rectangle(5, 145, 90, 50) };
        using var child = new Control { Name = "moving-child", Bounds = new Rectangle(5, 5, 30, 20) };
        source.Controls.Add(child);
        firstForm.Controls.Add(source);
        secondForm.Controls.Add(destination);
        if (!source.IsHandleCreated || !destination.IsHandleCreated || !child.IsHandleCreated)
            throw new InvalidOperationException("Hidden-parent reparenting requires already-created native windows.");
        nint childHandle = child.Handle;
        if (!IsWindow(childHandle) || !IsWindow(source.Handle) || !IsWindow(destination.Handle))
            throw new InvalidOperationException("Hidden-parent reparenting requires valid native HWNDs.");
        child.VisibleChanged += (_, _) => Events.Add($"child-visible:managed={child.Visible}:native={IsWindowVisible(childHandle)}:parent={Id(child.Parent)}");
        Record("native-hidden-reparent-initial", $"managed:{child.Visible}|native:{IsWindowVisible(childHandle)}|parent:{GetParent(childHandle) == source.Handle}");
        Observe("native-hide-source-parent", () => source.Visible = false, source, child);
        Record("native-hidden-reparent-hidden", $"managed:{child.Visible}|native:{IsWindowVisible(childHandle)}|parent:{GetParent(childHandle) == source.Handle}");
        Observe("native-reparent-from-hidden", () => child.Parent = destination, source, destination, child);
        Record("native-hidden-reparent-moved", $"managed:{child.Visible}|native:{IsWindowVisible(childHandle)}|parent:{GetParent(childHandle) == destination.Handle}|same-handle:{child.Handle == childHandle}|valid:{IsWindow(childHandle)}");
    }

    private static void NativeCrossThread(Form owner)
    {
        using var ready = new ManualResetEventSlim();
        using var finish = new ManualResetEventSlim();
        Control? foreignChild = null;
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            try
            {
                using var form = new Form { Name = "foreign-form", Text = "Cross-thread collection acceptance", Size = new Size(180, 120) };
                using var child = new Control { Name = "foreign-child", Size = new Size(20, 20) };
                form.Controls.Add(child);
                foreignChild = child;
                form.Show();
                ready.Set();
                // This scope tests native collection thread ownership. Multiple
                // Application.Run lifetimes are tracked separately in Control-03.
                if (!finish.Wait(10000)) throw new TimeoutException("Foreign window cleanup was not requested.");
                form.Close();
            }
            catch (Exception exception) { failure = exception; ready.Set(); }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        if (!ready.Wait(10000)) throw new TimeoutException("Foreign window did not show.");
        if (failure is not null) throw new InvalidOperationException("Foreign window failed.", failure);
        try
        {
            Record("cross-thread-real-handle", foreignChild is not null && IsWindow(foreignChild.Handle));
            Observe("cross-thread-native-add", () => owner.Controls.Add(foreignChild!), owner);
            Observe("cross-thread-native-range", () => owner.Controls.AddRange([foreignChild!]), owner);
        }
        finally
        {
            finish.Set();
            if (!thread.Join(10000)) throw new TimeoutException("Foreign window did not close.");
        }
        if (failure is not null) throw new InvalidOperationException("Foreign window exit failed.", failure);
    }

    private static void NativeSnapshot(string name, Control owner, params Control[] children)
    {
        var order = new List<string>();
        for (nint window = GetWindow(owner.Handle, 5); window != 0; window = GetWindow(window, 2))
        {
            Control? control = children.FirstOrDefault(child => child.Handle == window);
            order.Add(control is null ? "unknown" : control.Name);
            if (order.Count > 32) throw new InvalidOperationException("Native sibling traversal did not terminate.");
        }
        Record(name, $"managed:{Names(owner.Controls)}|native:{string.Join(",", order)}|parents:{children.All(child => GetParent(child.Handle) == owner.Handle)}");
    }

    private static TraceControl Traced(string name)
    {
        var control = new TraceControl { Name = name };
        control.ParentChanged += (_, _) => Events.Add($"event-parent:{Id(control)}:{Id(control.Parent)}");
        control.ControlAdded += (_, e) => Events.Add($"event-add:{Id(control)}:{Id(e.Control)}:{Id(e.Control?.Parent)}:{control.Controls.Count}");
        control.ControlRemoved += (_, e) => Events.Add($"event-remove:{Id(control)}:{Id(e.Control)}:{Id(e.Control?.Parent)}:{control.Controls.Count}");
        return control;
    }

    private static void Observe(string name, Action operation, params Control[] controls)
    {
        Events.Clear();
        Exception? failure = null;
        try { operation(); }
        catch (Exception exception) { failure = exception; }
        Record(name, $"exception:{ExceptionText(failure)}|events:{string.Join(";", Events)}|state:{string.Join(";", controls.Select(State))}");
    }

    private static string ExceptionText(Exception? exception) => exception is null ? "none" : ReferenceEquals(exception, Failure) ? "original" : exception.GetType().Name + (exception is ArgumentException argument ? ":" + (argument.ParamName ?? "null") : "");
    private static void RunGroup(string name, Action action)
    {
        Console.Error.WriteLine("BEGIN " + name);
        action();
        Console.Error.WriteLine("END " + name);
    }
    private static string State(Control control) => $"{Id(control)}:parent={Id(control.Parent)},children=[{Names(control.Controls)}],disposed={control.IsDisposed},disposing={control.Disposing},handle={control.IsHandleCreated}";
    private static string Names(IEnumerable controls) => string.Join(",", controls.Cast<Control>().Select(Id));
    private static string Id(Control? control) => control is null ? "null" : control.Name.Length == 0 ? "unnamed" : control.Name;
    private static string Rect(Rectangle bounds) => $"{bounds.X},{bounds.Y},{bounds.Width},{bounds.Height}";
    private static void Record(string name, object? value) => Console.WriteLine(name + "=" + (value?.ToString() ?? "null"));

    private class TraceControl : Control
    {
        protected override void OnParentChanged(EventArgs e)
        {
            Program.Events.Add($"on-parent-enter:{Id(this)}:{Id(Parent)}");
            base.OnParentChanged(e);
            Program.Events.Add($"on-parent-exit:{Id(this)}:{Id(Parent)}");
        }
        protected override void OnControlAdded(ControlEventArgs e)
        {
            Program.Events.Add($"on-add-enter:{Id(this)}:{Id(e.Control)}");
            base.OnControlAdded(e);
            Program.Events.Add($"on-add-exit:{Id(this)}:{Id(e.Control)}");
        }
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            Program.Events.Add($"on-remove-enter:{Id(this)}:{Id(e.Control)}");
            base.OnControlRemoved(e);
            Program.Events.Add($"on-remove-exit:{Id(this)}:{Id(e.Control)}");
        }
    }

    private sealed class FactoryControl : Control
    {
        internal int FactoryCalls;
        protected override ControlCollection CreateControlsInstance() { FactoryCalls++; return new DerivedCollection(this); }
    }

    private sealed class ThrowingFindControl : Control
    {
        internal Exception? GetterFailure;
        protected override ControlCollection CreateControlsInstance() => new ThrowingFindCollection(this);
    }

    private sealed class ThrowingFindCollection(ThrowingFindControl owner) : Control.ControlCollection(owner)
    {
        public override Control this[int index]
        {
            get
            {
                Program.Events.Add("find-index:" + index);
                if (index == 1 && owner.GetterFailure is { } failure) throw failure;
                return base[index];
            }
        }
    }

    private sealed class DerivedCollection(Control owner) : Control.ControlCollection(owner)
    {
        public override void Add(Control? value) { Program.Events.Add("collection-add:" + Id(value)); base.Add(value); }
        public override void AddRange(Control[] controls) { Program.Events.Add("collection-range"); base.AddRange(controls); }
        public override void Remove(Control? value) { Program.Events.Add("collection-remove:" + Id(value)); base.Remove(value); }
    }

    private sealed class ConstructorControl : Control
    {
        internal ConstructorControl(Control? parent, string? text) : base(parent: parent, text: text) { Program.Events.Add("ctor-body"); }
        internal ConstructorControl(Control? parent, string? text, int x, int y, int width, int height)
            : base(parent: parent, text: text, left: x, top: y, width: width, height: height) { Program.Events.Add("ctor-body"); }
        [AllowNull]
        public override string Text { get => base.Text; set { Program.Events.Add("ctor-text:" + (value ?? "null") + ":parent:" + Id(Parent)); base.Text = value; } }
        protected override void OnParentChanged(EventArgs e) { Program.Events.Add("ctor-parent:" + Id(Parent) + ":text:" + Text + ":bounds:" + Rect(Bounds)); base.OnParentChanged(e); }
        protected override void OnLocationChanged(EventArgs e) { Program.Events.Add("ctor-location:parent:" + Id(Parent)); base.OnLocationChanged(e); }
        protected override void OnSizeChanged(EventArgs e) { Program.Events.Add("ctor-size:parent:" + Id(Parent)); base.OnSizeChanged(e); }
    }

    [DllImport("user32.dll")]
    private static extern nint GetParent(nint hWnd);
    [DllImport("user32.dll")]
    private static extern nint GetWindow(nint hWnd, uint command);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint hWnd);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindowVisible(nint hWnd);
}

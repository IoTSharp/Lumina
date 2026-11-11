namespace Lumina.Forms;

public partial class ListView
{
    /// <summary>Standard nested collection identity retained alongside the historical top-level alias.</summary>
    public class ListViewItemCollection : global::Lumina.Forms.ListViewItemCollection
    {
        public ListViewItemCollection(ListView owner) : base(owner) { }
        internal ListViewItemCollection(ListViewGroup owner) : base(owner) { }
        public new System.Collections.IEnumerator GetEnumerator() => base.GetEnumerator();
    }

    public class ColumnHeaderCollection : global::Lumina.Forms.ColumnHeaderCollection
    {
        public ColumnHeaderCollection(ListView owner) : base(owner) { }
        public new System.Collections.IEnumerator GetEnumerator() => base.GetEnumerator();
    }
}

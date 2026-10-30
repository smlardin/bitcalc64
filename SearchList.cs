using System;
using System.Drawing;
using System.Windows.Forms;


public partial class SearchList : UserControl
{
    private bool mFilterMotion = false;
    private string mFilterText = "";

    public delegate void FilterTextChangedHandler(object Source, SearchListFilterEventArgs FilterArgs);

    public event FilterTextChangedHandler FilterTextChanged;

    // Aggregation values for the listbox to function like
    // a regular list
    public bool UseCompatibleStateImageBehavior
    {
        get { return embedList.UseCompatibleStateImageBehavior; }
        set { embedList.UseCompatibleStateImageBehavior = value; }
    }

    public System.Windows.Forms.View View
    {
        get { return embedList.View; }
        set { embedList.View = value; }
    }

    public bool VirtualMode
    {
        get { return embedList.VirtualMode; }
        set { embedList.VirtualMode = value; }
    }

    public bool HideSelection
    {
        get { return embedList.HideSelection; }
        set { embedList.HideSelection = value; }
    }

    public bool FullRowSelect
    {
        get { return embedList.FullRowSelect; }
        set { embedList.FullRowSelect = value; }
    }

    public int VirtualListSize
    {
        get { return embedList.VirtualListSize; }
        set { embedList.VirtualListSize = value; }
    }

    public System.Windows.Forms.ListView.ColumnHeaderCollection Columns
    {
        get { return embedList.Columns; }
    }

    public String FilterText
    {
        get { if (filterText.Text == "<Filter Text>") return ""; else return filterText.Text; }
        set { filterText.Text = value; UpdateFilterText(); }
    }

    // (Tricky) if we need this to act as a list, then we return the embedded list
    public static implicit operator ListView(SearchList inControl)
    {
        return inControl.embedList;
    }

    public ListView List
    {
        get { return embedList; }
    }

    //------------------------------------------------------------------
    // Non-derived function
    public SearchList()
    {
        InitializeComponent();

        // Default to hidden filter text
        mFilterMotion = true;
        UpdateFilterText();
        mFilterMotion = false;
    }

    private void UpdateFilterText()
    {
        if ((filterText.Text.Length == 0) || (filterText.Text == "<Filter Text>"))
        {
            filterText.Text = "<Filter Text>";
            filterText.ForeColor = SystemColors.InactiveCaptionText;
        }
        else
        {
            filterText.ForeColor = SystemColors.WindowText;
        }
    }

    private void SearchList_Layout(object sender, LayoutEventArgs e)
    {
        embedList.Top = filterText.Top + filterText.Height + 1;
        embedList.Height = (this.Height - (filterText.Height + 1)) - 2;
        embedList.Width = this.Width - embedList.Left - 1;
    }

    private void FilterText_Enter(object sender, EventArgs e)
    {
        mFilterMotion = true;
        filterText.ForeColor = SystemColors.WindowText;
        if ((filterText.Text.Length == 0) || (filterText.Text == "<Filter Text>"))
        {
            filterText.Text = "";
        }
        mFilterMotion = false;
    }

    private void FilterText_Leave(object sender, EventArgs e)
    {
        mFilterMotion = true;
        UpdateFilterText();
        mFilterMotion = false;
    }

    // Event so that we can filter the box in real time
    private void FilterText_TextChanged(object sender, EventArgs e)
    {
        if ((!mFilterMotion) && (FilterTextChanged != null) && (filterText.Text != mFilterText))
        {
            string text = mFilterText = filterText.Text;
            if (text == "<Filter Text>")
                text = "";

            SearchListFilterEventArgs filterargs = new SearchListFilterEventArgs(text);
            FilterTextChanged(this, filterargs);
        }
    }
}

public class SearchListFilterEventArgs : EventArgs
{
    public SearchListFilterEventArgs(string inFilterText)
    {
        FilterText = inFilterText;
    }

    public string FilterText { get; private set; }
}

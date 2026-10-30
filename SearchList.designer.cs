
partial class SearchList
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.embedList = new System.Windows.Forms.ListView();
        this.filterText = new System.Windows.Forms.TextBox();
        this.SuspendLayout();
        // 
        // embedList
        // 
        this.embedList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.embedList.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.embedList.Location = new System.Drawing.Point(0, 19);
        this.embedList.Name = "embedList";
        this.embedList.Size = new System.Drawing.Size(262, 264);
        this.embedList.TabIndex = 0;
        this.embedList.UseCompatibleStateImageBehavior = false;
        this.embedList.View = System.Windows.Forms.View.List;
        // 
        // filterText
        // 
        this.filterText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.filterText.BackColor = System.Drawing.Color.WhiteSmoke;
        this.filterText.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.filterText.Location = new System.Drawing.Point(0, 0);
        this.filterText.Name = "filterText";
        this.filterText.Size = new System.Drawing.Size(262, 13);
        this.filterText.TabIndex = 1;
        this.filterText.TextChanged += new System.EventHandler(this.FilterText_TextChanged);
        this.filterText.Leave += new System.EventHandler(this.FilterText_Leave);
        this.filterText.Enter += new System.EventHandler(this.FilterText_Enter);
        // 
        // SearchList
        // 
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
        this.BackColor = System.Drawing.Color.Gainsboro;
        this.Controls.Add(this.filterText);
        this.Controls.Add(this.embedList);
        this.Name = "SearchList";
        this.Size = new System.Drawing.Size(262, 283);
        this.Layout += new System.Windows.Forms.LayoutEventHandler(this.SearchList_Layout);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListView embedList;
    private System.Windows.Forms.TextBox filterText;
}

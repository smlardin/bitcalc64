using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

namespace BitCalc64
{
    public partial class Form1 : Form
    {
        System.Collections.Generic.List<BitTextExt> mBitTexts;
        readonly System.Collections.Generic.List<Color> mColors = new System.Collections.Generic.List<Color>();
        System.Collections.Generic.List<int> mNibbles = new System.Collections.Generic.List<int>(16);
        System.Collections.Generic.Stack<ulong> mHistory = new System.Collections.Generic.Stack<ulong>(8);
        System.Collections.Generic.List<String> mComment = new System.Collections.Generic.List<String>(64);

        UpdateTypes mChangeMode = UpdateTypes.cNone;

        const int bit_spacing = 16;

        BigInteger mBaseValue = 0;  // The one and only value

        int mLastGrid = -1;
        int mPageNumber = 1;
        int mPendingSave = 0;
        int mLeftBitCount = 64;

        int mFocusDirection = 1;
        int mLastFocus = 0;

        int mLastReflowStart = 0;
        int mLastReflowCount = 0;

        bool mUserMSBSwitch = false;

        CEditInfo mLastLabelEdit;

        Random mRandomGenerator = new Random();
        GridMode mGridMode = GridMode.cIBM;

        //const string mcToolsDir = "C:\\tools\\bits";
        const string mcNewBits = "-New Bits";
        const string mcComment = "-1";

        string scomAddress = "";

        // List of items in the list view
        SortedList<string, bool> mRegisterView = new SortedList<string, bool>();
        List<string> mRegisterList = new List<string>();

        string mFileName = "";

        // class for storing edit information about the currently editted label
        public class CEditInfo
        {
            public CEditInfo(int commentid, int editid)
            {
                mCommentID = commentid;
                mEditID = editid;
            }

            public int mCommentID;
            public int mEditID;
        };

        enum Columns
        {
            cStart = 0,
            cEnd = 1,
            cBinary = 5,
            cHex = 3,
            cDecimal = 4,
            cName = 2,
        };

        enum GridMode
        {
            cIBM,
            cIntel,
            cUnknown,
        };

        enum UpdateTypes
        {
            cNone,
            cPage,
            cBits,
            cMotoDec,
            cMotoHex,
            cGrid,
        };

        enum UpdateMode
        {
            cClear,
            cSet,
            cIncrement,
            cDecrement,
            cChange,
            cInvert,
        };

        public Form1()
        {
            InitializeComponent();

            filterList.FilterTextChanged += new SearchList.FilterTextChangedHandler(filterList_FilterTextChanged);
            filterList.List.VirtualItemsSelectionRangeChanged += new ListViewVirtualItemsSelectionRangeChangedEventHandler(List_VirtualItemsSelectionRangeChanged);
            filterList.List.SearchForVirtualItem += new SearchForVirtualItemEventHandler(List_SearchForVirtualItem);
            filterList.List.SelectedIndexChanged += new EventHandler(List_SelectedIndexChanged);
            filterList.List.KeyUp += new KeyEventHandler(List_KeyUp);
            filterList.List.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(List_RetrieveVirtualItem);
            filterList.List.MouseClick += new MouseEventHandler(List_MouseClick);

            filterList.Columns.Add("Items");
            filterList.Columns[0].Width = filterList.Width - 20;
            filterList.List.HeaderStyle = ColumnHeaderStyle.None;
        }

        void List_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (filterList.List.SelectedIndices.Count >= 1)
                {
                    int index = filterList.List.SelectedIndices[0];
                    string selectedItem = mRegisterList[index];
                    // TODO ContextMenu is no longer supported. Use ContextMenuStrip instead. For more details see https://docs.microsoft.com/en-us/dotnet/core/compatibility/winforms#removed-controls
                    filterList.ContextMenuStrip = new ContextMenuStrip();
                    if (selectedItem.StartsWith("0x"))
                    {
                        if (filterList.List.SelectedIndices.Count == 1)
                        {
                            filterList.ContextMenuStrip.Items.Add("Copy Address", null, List_CopyAddresses);
                        }
                        else
                        {
                            filterList.ContextMenuStrip.Items.Add("Copy Addresses", null, List_CopyAddresses);
                        }
                        filterList.ContextMenuStrip.Items.Add("Copy Full Text", null, List_CopyFull);
                        filterList.ContextMenuStrip.Items.Add("Copy Write Scom", null, List_CopyScomWrite);
                        filterList.ContextMenuStrip.Items.Add("Copy Read Scom", null, List_CopyScomRead);
                        // TODO MenuItem is no longer supported. Use ToolStripMenuItem instead. For more details see https://docs.microsoft.com/en-us/dotnet/core/compatibility/winforms#removed-controls
                        ToolStripMenuItem rmw = new ToolStripMenuItem("Copy RMW Scom...");
                        rmw.DropDownItems.Add("Generic", null, List_CopyScomRMW);
                        rmw.DropDownItems.Add("|/OR Value", null, List_CopyScomRMWOr);
                        rmw.DropDownItems.Add("&&/AND Value", null, List_CopyScomRMWAnd);
                        rmw.DropDownItems.Add("&&/AND NOT Value", null, List_CopyScomRMWAndNot);
                        filterList.ContextMenuStrip.Items.Add(rmw);
                    }

                    filterList.ContextMenuStrip.Items.Add("Open File Location", null, List_OpenFile);
                }
            }
        }

        void List_CopyScomRMW(string operation, string value)
        {
            string addresses = "";

            for (int i = 0; i < filterList.List.SelectedIndices.Count; i++)
            {
                string register = mRegisterList[filterList.List.SelectedIndices[i]];
                register = register.Substring(0, register.IndexOf('.'));

                if (addresses.Length > 0)
                    addresses += "\n";
                addresses += "read scom " + register + " temp_val\n";
                addresses += "set temp_val = temp_val " + operation + " " + value + "\n";
                addresses += "write scom " + register + " temp_val\n";
            }

            if (addresses.Length > 0)
            {
                try
                {
                    Clipboard.SetText(addresses);
                }
                catch (Exception ge)
                {
                }
            }

            return;
        }

        void List_CopyScomRMWAndNot(object sender, EventArgs e)
        {
            BigInteger inv_base = ~mBaseValue;
            List_CopyScomRMW("&", " 0x" + inv_base.ToString("X").PadLeft(16, '0'));
            return;
        }

        void List_CopyScomRMWAnd(object sender, EventArgs e)
        {
            List_CopyScomRMW("&", "0x" + hexMotorola.Text);
            return;
        }

        void List_CopyScomRMWOr(object sender, EventArgs e)
        {
            List_CopyScomRMW("|", "0x" + hexMotorola.Text);
            return;
        }

        void List_CopyScomRMW(object sender, EventArgs e)
        {
            List_CopyScomRMW("{operation}", "{value}");
            return;
        }

        void List_CopyScomWrite(object sender, EventArgs e)
        {
            string addresses = "";

            for (int i = 0; i < filterList.List.SelectedIndices.Count; i++)
            {
                string register = mRegisterList[filterList.List.SelectedIndices[i]];
                if (addresses.Length > 0)
                    addresses += "\n";
                addresses += "write scom " + register.Substring(0, register.IndexOf('.')) +
                    " 0x" + mBaseValue.ToString("X").PadLeft(16, '0');
            }

            if (addresses.Length > 0)
            {
                try
                {
                    Clipboard.SetText(addresses);
                }
                catch (Exception ge) { }
            }
            return;
        }

        void List_CopyScomRead(object sender, EventArgs e)
        {
            string addresses = "";

            for (int i = 0; i < filterList.List.SelectedIndices.Count; i++)
            {
                string register = mRegisterList[filterList.List.SelectedIndices[i]];
                if (addresses.Length > 0)
                    addresses += "\n";
                addresses += "read scom " + register.Substring(0, register.IndexOf('.'));
            }

            if (addresses.Length > 0)
            {
                try
                {
                    Clipboard.SetText(addresses);
                }
                catch (Exception ge) { }
            }
            return;
        }

        void List_CopyFull(object sender, EventArgs e)
        {
            string addresses = "";

            for (int i = 0; i < filterList.List.SelectedIndices.Count; i++)
            {
                string register = mRegisterList[filterList.List.SelectedIndices[i]];
                if (addresses.Length > 0)
                    addresses += "\n";
                addresses += register;
            }

            if (addresses.Length > 0)
            {
                try
                {
                    Clipboard.SetText(addresses);
                }
                catch (Exception ge)
                {
                }
            }

            return;
        }

        void List_CopyAddresses(object sender, EventArgs e)
        {
            string addresses = "";

            for (int i = 0; i < filterList.List.SelectedIndices.Count; i++)
            {
                string register = mRegisterList[filterList.List.SelectedIndices[i]];
                if (addresses.Length > 0)
                    addresses += "\n";
                addresses += register.Substring(0, register.IndexOf('.'));
            }

            if (addresses.Length > 0)
            {
                try
                {
                    Clipboard.SetText(addresses);
                }
                catch (Exception ge)
                {
                }
            }

            return;
        }

        void List_OpenFile(object sender, EventArgs e)
        {
            if (filterList.List.SelectedIndices.Count > 0)
            {
                // can you just open a window to view the file directly?
                string fileName =
                    getBitFileName(mRegisterList[filterList.List.SelectedIndices[0]]);
                if (fileName == "")
                    return;

                // execute the file using the shellexecute
                try
                {
                    System.Diagnostics.Process.Start(fileName);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.EnableRaisingEvents = false;
                    proc.StartInfo.Arguments = "/select," + fileName;
                    proc.StartInfo.FileName = "explorer.exe";
                    proc.Start();
                }
            }
        }

        void List_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex > mRegisterList.Count)
            {
                e.Item = new ListViewItem("");
            }

            e.Item = new ListViewItem(mRegisterList[e.ItemIndex]);
        }

        void List_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void List_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filterList.List.SelectedIndices.Count > 0)
            {
                readBitFile(mRegisterList[filterList.List.SelectedIndices[0]]);
                scomAddress = mRegisterList[filterList.List.SelectedIndices[0]];
                if (scomAddress.IndexOf('.') > 0)
                    scomAddress = scomAddress.Substring(0, scomAddress.IndexOf('.'));
            }
        }

        void List_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void List_VirtualItemsSelectionRangeChanged(object sender, ListViewVirtualItemsSelectionRangeChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void filterList_FilterTextChanged(object Source, SearchListFilterEventArgs FilterArgs)
        {
            try
            {
                Regex filterReg = new Regex(FilterArgs.FilterText, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

                mRegisterList.Clear();
                foreach (string item in mRegisterView.Keys)
                {
                    if (filterReg.IsMatch(item))
                        mRegisterList.Add(item);
                }

                filterList.VirtualListSize = mRegisterList.Count;
            }
            catch (Exception e)
            {
            }
            return;
        }

        private void PreLoadList()
        {
            string[] files;
            int start;

            mRegisterView.Clear();
            try
            {
                files = Directory.GetFiles(System.IO.Directory.GetCurrentDirectory() + "\\bits", "*.bits");
                start = (System.IO.Directory.GetCurrentDirectory() + "\\bits").Length;

                for (int i = 0; i < files.GetLength(0); ++i)
                    mRegisterView.Add(files[i].Substring(start + 1, files[i].Length - (5 + start + 1)), true);
            }
            catch (DirectoryNotFoundException dnfe)
            {
                files = Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), "*.bits");
                start = System.IO.Directory.GetCurrentDirectory().Length;
            }

            try
            {
                files = Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), "*.bits");
                start = System.IO.Directory.GetCurrentDirectory().Length;
                predefBitsCombo.Items.Add(mcNewBits);
                for (int i = 0; i < files.GetLength(0); ++i)
                    predefBitsCombo.Items.Add(files[i].Substring(start + 1, files[i].Length - (5 + start + 1)));
            }
            catch (DirectoryNotFoundException dnfe)
            {
            }

            filterList.VirtualListSize = 0;
            mRegisterList.Clear();
            foreach (string regname in mRegisterView.Keys)
                mRegisterList.Add(regname);
            filterList.VirtualListSize = mRegisterList.Count;

            return;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mBitTexts = new System.Collections.Generic.List<BitTextExt>();

            for (int i = 1; i <= 64; i++)
            {
                mBitTexts.Add(new BitTextExt());

                this.Controls.Add(mBitTexts[i - 1]);

                mBitTexts[i - 1].Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                mBitTexts[i - 1].Id = i - 1;
                mBitTexts[i - 1].Location = new Point(3 + (i - 1) * bit_spacing, 63);
                mBitTexts[i - 1].Margin = new Padding(2);
                mBitTexts[i - 1].Position = BitCalc64.BitTextExt.BitPosition.cNone;
                mBitTexts[i - 1].Size = new Size(bit_spacing, 22);
                mBitTexts[i - 1].TabIndex = 256 + (i - 1);
                mBitTexts[i - 1].TextAlign = System.Drawing.ContentAlignment.BottomCenter;
                mBitTexts[i - 1].Name = "textbox" + i.ToString();

                mBitTexts[i - 1].TextChanged += new EventHandler(Form1_TextChanged);
                mBitTexts[i - 1].DoubleClick += new EventHandler(Form1_TextDoubleClick);
                mBitTexts[i - 1].KeyPress += new KeyPressEventHandler(Form1_TextKeyPress);
                mBitTexts[i - 1].RowSelected += new BitTextExt.RowSelectionHandler(Form1_TextRowSelected);
                mBitTexts[i - 1].Id = i - 1;

                // Make sure we are using the most real estate possible
                //if (mBitTexts[i - 1].Top != 0)
                //{
                //    mBitTexts[i - 1].Height += mBitTexts[i - 1].Top;
                //    mBitTexts[i - 1].Top = 0;
                //}

                mBitTexts[i - 1].MouseClick += new MouseEventHandler(Form1_BitLabelMouseClick);

                // Have to add twice since we are only running the loop once
                mComment.Add("");
                mComment.Add("");

                // Set the pointers so the boxes know who they are related to
                if (i >= 2)
                {
                    mBitTexts[i - 1].Prev = mBitTexts[i - 2];
                    mBitTexts[i - 2].Next = mBitTexts[i - 1];
                }

                if (i <= mNibbles.Capacity)
                    mNibbles.Add(0);
            }

            mColors.Add(Color.Thistle);
            mColors.Add(Color.PaleGreen);
            mColors.Add(Color.PaleTurquoise);
            mColors.Add(Color.MistyRose);
            mColors.Add(Color.PaleGoldenrod);

            Update_Labels();
            this.Text = this.Text + "  v" + Application.ProductVersion;
            PreLoadList();
            Update(UpdateTypes.cPage);
            predefBitsCombo.Text = mcNewBits;

            ReflowBits();
        }

        // When the label is clicked we want to show an edit textbox to in-line edit the label
        void Form1_BitLabelMouseClick(object sender, MouseEventArgs e)
        {
            BitTextExt label = sender as BitTextExt;

            if (labelEdit.Visible)
            {
                mComment[mLastLabelEdit.mCommentID] = labelEdit.Text.Trim(" \n\r".ToCharArray());
                Update_Label(mLastLabelEdit.mEditID);

                // A save of the comments is in order
                Save_Grid();
            }

            labelEdit.Location = label.Location;
            labelEdit.Size = mBitTexts[0].Size;
            labelEdit.Left -= 4;
            labelEdit.Width = mBitTexts[0].Width + 76;
            labelEdit.Top = 0;
            labelEdit.Height = mBitTexts[0].Top - mBitTexts[0].Height;
            labelEdit.Visible = true;
            labelEdit.Focus();

            mLastLabelEdit = (CEditInfo)label.Tag;
            labelEdit.Text = mComment[mLastLabelEdit.mCommentID];
            labelEdit.SelectAll();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (mBitTexts.Count <= 0)
                return;

            for (int i = 0; i < mBitTexts.Count; ++i)
            {
                // Draw all of the labels on the form using the label callback
                if (!mBitTexts[i].Visible)
                    continue;

                mBitTexts[i].DrawLabel(e.Graphics);

                if ((i & 0x3) == 0x3)
                {
                    Point child = mBitTexts[i].Location;
                    child.X = child.X;
                    child.Y = child.Y;

                    int x = child.X + mBitTexts[i].Width;
                    int y = child.Y + mBitTexts[i].Height + 2;
                    if (i != mBitTexts.Count - 1)
                        e.Graphics.FillRectangle(Brushes.Gray, x, y, 2, 15);

                    e.Graphics.DrawString(mNibbles[i / 4].ToString("X"), this.Font, Brushes.Gray, new RectangleF(x - (bit_spacing * 2) - (bit_spacing / 2), y, 20, 20));
                }
            }
        }

        void Form1_TextRowSelected(object sender, RowEvent e)
        {
            mLastGrid = -1;

            for (int i = 0; i < bitGridView.Rows.Count; ++i)
            {
                if ((bitGridView.Rows[i].Cells[0].Value != null) && (bitGridView.Rows[i].Cells[1].Value != null))
                {
                    bitGridView.Rows[i].Selected = ((bitGridView.Rows[i].Cells[0].Value.ToString() == e.Row.Start.ToString()) &&
                        (bitGridView.Rows[i].Cells[1].Value.ToString() == e.Row.End.ToString()));
                    if (bitGridView.Rows[i].Selected)
                        bitGridView.FirstDisplayedScrollingRowIndex = i;
                }
                mLastGrid = bitGridView.Rows[i].Selected ? i : mLastGrid;
            }

            if (mLastFocus < e.Id)
                mFocusDirection = 1;
            else
                mFocusDirection = -1;
            mLastFocus = e.Id;
        }

        void Form1_TextDoubleClick(object sender, EventArgs e)
        {
            BitTextExt tb = sender as BitTextExt;

            if (tb.Text != "0")
            {
                tb.Text = "0";
            }
            else
            {
                tb.Text = "1";
            }
        }

        void Form1_TextChanged(object sender, EventArgs e)
        {
            int realbit;
            int bit;

            if (Updating())
                return;

            realbit = bit = int.Parse((sender as BitTextExt).Name.Substring(7)) - 1;

            int nibblemask = 1 << (3 - (bit % 4));

            mNibbles[bit / 4] |= nibblemask;
            mNibbles[bit / 4] &= mBitTexts[bit].Text != "0" ? ~0 : ~nibblemask;

            string bitval = (sender as BitTextExt).Text;
            if (bitval.Length > 1)
                bitval = (sender as BitTextExt).Text = bitval.Substring(bitval.Length - 1, 1);

            if (bitval != "0")
            {
                mBaseValue |= (BigInteger.One << (63 - bit));
                if (bitval != "1")
                    (sender as BitTextExt).Text = "1";
            }
            else
            {
                mBaseValue &= ~(BigInteger.One << (63 - bit));
            }


            Update(UpdateTypes.cBits);

            return;
        }

        private void Form1_TextKeyPress(Object sender, EventArgs e)
        {
            int realbit;

            realbit = (sender as BitTextExt).Id;            // Move focus to the next control allows easy entry
            if ((realbit != 63) && (mFocusDirection > 0))
                mBitTexts[realbit + 1].Focus();
            else if ((realbit > 0) && (mFocusDirection < 0))
                mBitTexts[realbit - 1].Focus();
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            mPageNumber = 1 - mPageNumber;
            this.Focus();
            ReflowBits();
            Update_Labels();
            Update(UpdateTypes.cMotoHex);
        }

        private bool Updating()
        {
            if (mChangeMode != UpdateTypes.cNone)
                return true;

            return false;
        }

        private void Update(UpdateTypes updating)
        {
            if (mChangeMode != UpdateTypes.cNone)
                return;

            this.SuspendLayout();
            mChangeMode = updating;

            if (updating != UpdateTypes.cBits)
                Update_Bits();
            if (updating != UpdateTypes.cMotoDec)
                Update_MotoDec();
            if (updating != UpdateTypes.cMotoHex)
                Update_MotoHex();
            if (updating != UpdateTypes.cPage)
                Update_PageCheck();
            if (updating != UpdateTypes.cGrid)
                Update_Grid();

            this.ResumeLayout();
            this.Invalidate(false);
            mChangeMode = UpdateTypes.cNone;

            return;
        }

        private void Update_PageCheck()
        {
            if (mPageNumber != 0)
                highBitsCheck.Checked = false;
            else
                highBitsCheck.Checked = true;
        }

        private void Update_Label(int i)
        {
            string top, left, right;
            int index = -1;
            int crcount = 0;

            // count the number of cr in the comment
            top = mComment[(63 - i)];
            while ((index = top.IndexOf("\n", index + 1)) >= 0)
                crcount++;

            // append additionals to give even spacing
            while (crcount < 2)
            {
                top += "\n";
                crcount++;
            }

            if (mLeftBitCount > (63 - i))
            {
                left = (i - (64 - mLeftBitCount)).ToString();
                right = "\n" + (63 - i).ToString();
            }
            else
            {
                left = "";
                right = "\n";
            }

            mBitTexts[i].LabelText = top + left + right;
            mBitTexts[i].Tag = new CEditInfo((63 - i), i);

            Rectangle r = new Rectangle(0, 0, mBitTexts[0].Top + mBitTexts[0].Height, this.Width);
            Invalidate(r);
        }

        private void Update_Labels()
        {
            for (int i = 0; i < 64; i++)
            {
                Update_Label(i);
            }
        }

        private void Update_Bits()
        {
            // Clear the nibble counters
            for (int i = 0; i < mNibbles.Count; i++)
                mNibbles[i] = 0;

            // Reset all of the background colors on the bits
            for (int i = 0; i < 64; i++)
            {
                string bitvalue = ((mBaseValue & (BigInteger.One << (63 - i))) != 0) ? "1" : "0";
                if (bitvalue != mBitTexts[i].Text)
                    mBitTexts[i].Text = bitvalue;
                mBitTexts[i].BackColor = Color.White;
                mBitTexts[i].Position = BitTextExt.BitPosition.cNone;
                mBitTexts[i].Row = new RowId(-1, -1);

                mNibbles[i / 4] <<= 1;
                mNibbles[i / 4] |= (mBitTexts[i].Text != "0") ? 1 : 0;
            }
        }

        private void Update_MotoDec()
        {
            decMotorola.Text = mBaseValue.ToString();
        }

        private void Update_MotoHex()
        {
            hexMotorola.Text = mBaseValue.ToString("X").TrimStart('0');
        }

        private void hexMotorola_TextChanged(object sender, EventArgs e)
        {
            string value = hexMotorola.Text;

            if (Updating())
                return;

            // Remove an embedded : since 64 bit IBM numbers sometimes have those...
            int index;
            while ((index = value.IndexOfAny(":_ ".ToCharArray())) != -1)
                value = value.Remove(index, 1);

            // Remove a starting 0x from the number
            if (value.ToLower().StartsWith("0x"))
                value = value.Remove(0, 2);
            if (value.ToLower().StartsWith("0h"))
                value = value.Remove(0, 2);
            if (value.ToLower().StartsWith("h"))
                value = value.Remove(0, 1);

            // BigInteger.parse always interprets the value as a sign-extended negative if it starts with the high bit 1, so prefix with 0
            value = "0" + value;

            mBaseValue = 0;

            // May throw an exception and we don't want that...
            try
            {
                mBaseValue = BigInteger.Parse(value, System.Globalization.NumberStyles.HexNumber);
            }
            catch (System.Exception exception)
            {
            }
            Update(UpdateTypes.cMotoHex);
        }

        private void decMotorola_TextChanged(object sender, EventArgs e)
        {
            if (Updating())
                return;

            mBaseValue = 0;
            try
            {
                string value = decMotorola.Text;
                // Remove an embedded : since 64 bit IBM numbers sometimes have those...
                int index;
                while ((index = value.IndexOfAny(",".ToCharArray())) != -1)
                    value = value.Remove(index, 1);

                mBaseValue = BigInteger.Parse(value);
            }
            catch (System.Exception exception)
            {
            }
            Update(UpdateTypes.cMotoDec);
        }

        private void clearAllButton_Click(object sender, EventArgs e)
        {
            mBaseValue = 0;
            Update(UpdateTypes.cPage);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mBaseValue = ulong.MaxValue;
            Update(UpdateTypes.cPage);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mBaseValue = ~mBaseValue;
            Update(UpdateTypes.cPage);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mBaseValue &= ((BigInteger)uint.MaxValue) << (mPageNumber * 32);
            Update(UpdateTypes.cPage);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mBaseValue |= ((BigInteger)uint.MaxValue) << ((1 - mPageNumber) * 32);
            Update(UpdateTypes.cPage);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mBaseValue ^= ((BigInteger)uint.MaxValue) << ((1 - mPageNumber) * 32);
            Update(UpdateTypes.cPage);
        }

        private void shiftLeft_Click(object sender, EventArgs e)
        {
            try
            {
                int bits = int.Parse(shiftBits.Text);
                mBaseValue <<= bits;
                Update(UpdateTypes.cPage);
            }
            catch (Exception ex)
            {
            }
        }

        private void shiftRight_Click(object sender, EventArgs e)
        {
            try
            {
                int bits = int.Parse(shiftBits.Text);
                mBaseValue >>= bits;
                Update(UpdateTypes.cPage);
            }
            catch (Exception ex)
            {
            }
        }

        private void highBitsCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (Updating())
                return;

            if ((sender as CheckBox).Checked)
                mPageNumber = 0;
            else
                mPageNumber = 1;

            ReflowBits();
            Update(UpdateTypes.cPage);
        }

        private void setRowButton_Click(object sender, EventArgs e)
        {
            Update_FromValue(mLastGrid, Columns.cDecimal, UpdateMode.cSet);
        }

        private void clearRowButton_Click(object sender, EventArgs e)
        {
            Update_FromValue(mLastGrid, Columns.cDecimal, UpdateMode.cClear);
        }

        private void incRowButton_Click(object sender, EventArgs e)
        {
            Update_FromValue(mLastGrid, Columns.cDecimal, UpdateMode.cIncrement);
        }

        private void decRowButton_Click(object sender, EventArgs e)
        {
            Update_FromValue(mLastGrid, Columns.cDecimal, UpdateMode.cDecrement);
        }

        private void invertRowButton_Click(object sender, EventArgs e)
        {
            Update_FromValue(mLastGrid, Columns.cDecimal, UpdateMode.cInvert);
        }

        private void bitGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (int)Columns.cStart || e.ColumnIndex == (int)Columns.cEnd || e.ColumnIndex == (int)Columns.cName)
            {
                Update_Grid();
                Save_Grid();
            }
            else
                Update_FromValue(e.RowIndex, (Columns)e.ColumnIndex, UpdateMode.cChange);

            return;
        }

        private void bitGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            mLastGrid = e.RowIndex;

            return;
        }

        // Update when either the binary, decimal, or hex changes in the grid
        private void Update_FromValue(int row, Columns column, UpdateMode umode)
        {
            int start = 0, end = 0;
            if (GetGridStartEnd(row, ref start, ref end))
            {
                int bits = GetBitCount(start, end);
                BigInteger mask = GetMask(bits);
                int shift = GetShift(start, end);
                BigInteger value = 0;

                if (column == Columns.cBinary)
                    value = GetBinaryValue(bitGridView[(int)Columns.cBinary, row].Value.ToString());
                else if (column == Columns.cHex)
                    value = GetHexValue(bitGridView[(int)Columns.cHex, row].Value.ToString());
                else
                    value = GetDecValue(bitGridView[(int)Columns.cDecimal, row].Value.ToString());

                switch (umode)
                {
                    case UpdateMode.cChange:
                        break;
                    case UpdateMode.cClear:
                        value = 0;
                        break;
                    case UpdateMode.cDecrement:
                        value -= 1;
                        break;
                    case UpdateMode.cIncrement:
                        value += 1;
                        break;
                    case UpdateMode.cInvert:
                        value = ~value;
                        break;
                    case UpdateMode.cSet:
                        value = ulong.MaxValue;
                        break;
                }
                // Remove what was there and put the new value into place
                mBaseValue &= ~(mask << shift);
                value &= mask;
                mBaseValue |= (value << shift);

                Update(UpdateTypes.cPage);
            }
        }

        // Get the grid start and end bits
        private bool GetGridStartEnd(int row, ref int start, ref int end)
        {
            bool success = false;

            start = 0;
            end = 0;

            if ((row >= bitGridView.Rows.Count) || (row < 0))
                return false;

            DataGridViewCell startc = bitGridView[(int)Columns.cStart, row];
            DataGridViewCell endc = bitGridView[(int)Columns.cEnd, row];

            if ((startc == null) || (endc == null))
                return false;
            if ((startc.Value == null) || (endc.Value == null))
                return false;

            try
            {
                start = int.Parse(startc.Value.ToString());
                end = int.Parse(endc.Value.ToString());
                success = true;
            }
            catch (Exception ex)
            {
            }

            // Allow cells with bit start greater than 63 (for 128-bit numbers)
            if ((start > 63) && (end > 63))
            {
                start -= 64;
                end -= 64;
            }

            if ((start < 0) || (start > 63) || (end < 0) || (end > 63))
                return false;

            if (start < end)
            {
                if (!mUserMSBSwitch)
                {
                    mGridMode = GridMode.cIBM;
                    msbBit0.Checked = true;
                }
                else if (mGridMode == GridMode.cIntel)
                {
                    int swap = start;
                    start = end;
                    end = swap;
                }
            }
            else if (start > end)
            {
                if (!mUserMSBSwitch)
                {
                    mGridMode = GridMode.cIntel;
                    msbBit0.Checked = false;
                }
                else if (mGridMode == GridMode.cIBM)
                {
                    int swap = start;
                    start = end;
                    end = swap;
                }
            }

            return success;
        }

        // Save grid numbers to the current file name
        private void Save_Grid()
        {
            if ((mFileName.Length > 0) && (mFileName != mcNewBits + ".bits") &&
                (mFileName != ".bits"))
            {
                try
                {
                    //FileStream fs = File.Open(predefBitsCombo.Text + ".bits", FileMode.Create, FileAccess.Write);
                    FileStream fs = File.Open(mFileName, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);

                    int start = 0, end = 0;
                    string name;

                    // store the left bit count if we have one
                    if ((ibmBitCount.Text.Trim() != "64") && (ibmBitCount.Text.Trim() != ""))
                        sw.WriteLine("LeftBit=" + ibmBitCount.Text.Trim());

                    for (int i = 0; i < bitGridView.RowCount; i++)
                    {
                        if (!GetGridStartEnd(i, ref start, ref end))
                            continue;

                        if (bitGridView[(int)Columns.cName, i].Value == null)
                            name = "";
                        else
                            name = bitGridView[(int)Columns.cName, i].Value.ToString();

                        sw.WriteLine(start.ToString() + "," + end.ToString() + "," + name);
                    }
                    for (int i = 0; i < mComment.Count; i++)
                    {
                        if (mComment[i].Length > 0)
                            sw.WriteLine(i.ToString() + "," + mcComment + "," + HttpUtility.UrlEncode(mComment[i]));
                    }

                    if (!predefBitsCombo.Items.Contains(predefBitsCombo.Text))
                        predefBitsCombo.Items.Add(predefBitsCombo.Text);

                    sw.Close();
                    fs.Close();
                    mPendingSave = 0;
                }
                catch (Exception exp)
                {
                }
            }
            else
            {
                mPendingSave = 5;
            }
        }

        // Update all of the fields in the column
        private void Update_Grid()
        {
            int start = 0, end = 0;

            for (int i = 0; i < bitGridView.RowCount; i++)
            {
                string binValue = "";
                string hexValue = "";
                string decValue = "";

                // Row not available
                if (!GetGridStartEnd(i, ref start, ref end))
                {
                    // Clear the grid values for the row
                    bitGridView[(int)Columns.cBinary, i].Value = "";
                    bitGridView[(int)Columns.cHex, i].Value = "";
                    bitGridView[(int)Columns.cDecimal, i].Value = "";
                    continue;
                }

                BigInteger value;
                if (mGridMode == GridMode.cIBM)
                    value = mBaseValue >> ((mLeftBitCount - 1) - end);
                else
                    value = mBaseValue >> end;

                int bits = GetBitCount(start, end);
                value &= GetMask(bits);

                binValue = GetBits(value, bits);

                if (mGridMode == GridMode.cIBM)
                {
                    if (bitGridView[(int)Columns.cBinary, i].Style.Alignment != DataGridViewContentAlignment.MiddleLeft)
                        bitGridView[(int)Columns.cBinary, i].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                else
                {
                    if (bitGridView[(int)Columns.cBinary, i].Style.Alignment != DataGridViewContentAlignment.MiddleRight)
                        bitGridView[(int)Columns.cBinary, i].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                hexValue = value.ToString("X");
                decValue = value.ToString();

                if ((string)bitGridView[(int)Columns.cDecimal, i].Value != decValue) bitGridView[(int)Columns.cDecimal, i].Value = decValue;
                if ((string)bitGridView[(int)Columns.cHex, i].Value != hexValue) bitGridView[(int)Columns.cHex, i].Value = hexValue;
                if ((string)bitGridView[(int)Columns.cBinary, i].Value != binValue) bitGridView[(int)Columns.cBinary, i].Value = binValue;

                Colorize(i, start, end);
            }
        }

        private int GetShift(int start, int end)
        {
            if (mGridMode == GridMode.cIBM)
                return (mLeftBitCount - 1) - end;

            return end;
        }

        private int GetBitCount(int start, int end)
        {
            int bits = 0;
            if (mGridMode == GridMode.cIBM)
                bits = (end - start + 1);
            else
                bits = (start - end + 1);

            return bits;
        }

        private BigInteger GetMask(int bits)
        {
            BigInteger mask = 0;
            for (int c = 0; c < bits; c++)
            {
                mask <<= 1;
                mask |= 1;
            }

            return mask;
        }

        private string GetBits(BigInteger value, int count)
        {
            string bits = "";
            for (int i = 0; i < count; ++i)
            {
                if ((value & 0x1) != 0)
                    bits = "1" + bits;
                else
                    bits = "0" + bits;

                value >>= 1;
            }

            return bits;
        }

        public string getBitFileName(string bitFileName)
        {
            if (File.Exists(bitFileName + ".bits"))
                return bitFileName + ".bits";
            else if (File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\bits\\" +
                bitFileName + ".bits"))
                return System.IO.Directory.GetCurrentDirectory() + "\\bits\\" + bitFileName + ".bits";

            return "";
        }

        private void readBitFile(string bitFileName)
        {
            int rowcount = 0;
            bitGridView.SuspendLayout();
            bitGridView.Rows.Clear();

            mPendingSave = 0;

            // Clear the comments
            for (int i = 0; i < mComment.Count; i++)
                mComment[i] = "";

            try
            {
                mFileName = getBitFileName(bitFileName);
                if (mFileName == "")
                    return;

                StreamReader sr = new StreamReader(mFileName);

                int start = 0, end = 0;
                int last = 0;
                string line;

                // hardcode to always be 64-bits
                last = 0;
                if (last != 0)
                    ibmBitCount.Text = last.ToString();
                else
                    ibmBitCount.Text = "64";

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if ((line.Length > 0) && (line.Contains(",")))
                    {
                        string[] split = line.Split(",".ToCharArray());
                        if (split.Length > 1)
                        {
                            if (split[1] == mcComment)
                                mComment[int.Parse(split[0])] = HttpUtility.UrlDecode(split[2]);
                            else
                            {
                                int row = bitGridView.Rows.Add();
                                rowcount++;

                                bitGridView[(int)Columns.cStart, row].Value = split[0];
                                bitGridView[(int)Columns.cEnd, row].Value = split[1];

                                if (split.Length > 2)
                                    bitGridView[(int)Columns.cName, row].Value = split[2];

                                GetGridStartEnd(row, ref start, ref end);
                                if ((mGridMode == GridMode.cIBM) && (last <= end))
                                    last = end + 1;
                            }
                        }
                    }
                    else if (line.StartsWith("LeftBit="))
                    {
                        ibmBitCount.Text = line.Split("=".ToCharArray())[1].Trim();
                    }
                }
                sr.Close();

                // Update the labels and the rest of the form
                Update(UpdateTypes.cPage);
                Update_Labels();
                bitGridView.ResumeLayout();
            }
            catch (Exception exp)
            {
            }

            return;
        }

        private void predefBitsCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            string cursel = predefBitsCombo.SelectedItem.ToString();

            // Disable the script button
            scomAddress = "";

            // New bits is special
            if (cursel == mcNewBits)
            {
                bitGridView.SuspendLayout();
                bitGridView.Rows.Clear();
                mPendingSave = 0;
                // Clear the comments
                for (int i = 0; i < mComment.Count; i++)
                    mComment[i] = "";
                bitGridView.ResumeLayout();

                Update(UpdateTypes.cPage);
                Update_Labels();
                return;
            }

            readBitFile(cursel);
        }

        private BigInteger GetBinaryValue(string bits)
        {
            BigInteger value = 0;
            for (int i = 0; i < bits.Length; ++i)
            {
                // skip spaces since we want to do the right thing
                if (bits[i].Equals(' '))
                    continue;

                value <<= 1;
                if (!bits[i].Equals('0'))
                    value |= 1;
            }
            return value;
        }

        private BigInteger GetHexValue(string hex)
        {
            BigInteger value = 0;
            try
            {
                // prefix so BigInteger.Parse doesn't treat as a sign extended negative
                hex = "0" + hex;
                value = BigInteger.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            }
            catch (Exception exp)
            {

            }
            return value;
        }

        private BigInteger GetDecValue(string dec)
        {
            BigInteger value = 0;
            try
            {
                value = BigInteger.Parse(dec);
            }
            catch (Exception exp)
            {

            }
            return value;
        }

        private void saveTimer_Tick(object sender, EventArgs e)
        {
            if (mPendingSave != 0)
            {
                mPendingSave -= 1;
                if (mPendingSave == 0)
                    Save_Grid();
            }

            UpdateSaveButtonState();
        }

        private void predefBitsCombo_TextChanged(object sender, EventArgs e)
        {
            mPendingSave = 5;
            mFileName = predefBitsCombo.Text + ".bits";
        }

        private void bitGridView_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if ((e.Column.Index == (int)Columns.cStart) || (e.Column.Index == (int)Columns.cEnd) || (e.Column.Index == (int)Columns.cDecimal))
            {
                uint v1 = 0, v2 = 0;

                try
                {
                    v1 = uint.Parse(e.CellValue1.ToString());
                    v2 = uint.Parse(e.CellValue2.ToString());
                    e.SortResult = 1;
                    if (v1 < v2) e.SortResult = -1;
                    if (v1 == v2) e.SortResult = 0;
                }
                catch (Exception exp)
                {
                    e.SortResult = String.Compare(e.CellValue1.ToString(), e.CellValue2.ToString());
                }
            }
            else
            {
                if ((e.CellValue1 != null) && (e.CellValue2 != null))
                    e.SortResult = String.Compare(e.CellValue1.ToString(), e.CellValue2.ToString());
                else if (e.CellValue1 != null)
                    e.SortResult = 1;
                else if (e.CellValue2 != null)
                    e.SortResult = -1;
                else
                    e.SortResult = 0;
            }
            e.Handled = true;
        }

        private void ibmBitCount_TextChanged(object sender, EventArgs e)
        {
            mLeftBitCount = 0;
            try
            {
                mLeftBitCount = int.Parse(ibmBitCount.Text.Trim());
                if ((mLeftBitCount < 0) || (mLeftBitCount > 64))
                    mLeftBitCount = 64;

                mPendingSave = 5;

                Update_Labels();
                Update(UpdateTypes.cPage);

            }
            catch (Exception exp)
            {
            }
        }

        private void bitGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            mLastGrid = e.RowIndex;
        }

        private void Colorize(int row, int start, int end)
        {
            int maxVisibleBit = GetMaxVisibleBitIndex();

            if (mGridMode == GridMode.cIBM)
            {
                for (int i = start; i <= end; i++)
                {
                    int displaybit = 64 - (mLeftBitCount - i);

                    if ((displaybit < 0) || (displaybit > maxVisibleBit) || (displaybit >= mBitTexts.Count))
                        continue;

                    mBitTexts[displaybit].BackColor = mColors[row % mColors.Count];
                    mBitTexts[displaybit].Row = new RowId(start, end);
                    if ((i == start) && (i == end))
                        mBitTexts[displaybit].Position = BitTextExt.BitPosition.cOrphan;
                    else if (i == start)
                        mBitTexts[displaybit].Position = BitTextExt.BitPosition.cLeft;
                    else if (i == end)
                        mBitTexts[displaybit].Position = BitTextExt.BitPosition.cRight;
                    else
                        mBitTexts[displaybit].Position = BitTextExt.BitPosition.cMiddle;
                }
            }
            else
            {
                for (int i = end; i <= start; i++)
                {
                    int displaybit = 63 - i;

                    if ((displaybit < 0) || (displaybit > maxVisibleBit) || (displaybit >= mBitTexts.Count))
                        continue;

                    mBitTexts[displaybit].BackColor = mColors[row % mColors.Count];
                    mBitTexts[displaybit].Row = new RowId(start, end);
                    if ((i == start) && (i == end))
                        mBitTexts[displaybit].Position = BitTextExt.BitPosition.cOrphan;
                    else if (i == start)
                        mBitTexts[displaybit].Position = BitTextExt.BitPosition.cLeft;
                    else if (i == end)
                        mBitTexts[displaybit].Position = BitTextExt.BitPosition.cRight;
                    else
                        mBitTexts[displaybit].Position = BitTextExt.BitPosition.cMiddle;
                }
            }

            return;
        }

        private void ReflowBits()
        {
            int width = this.Width;
            int bitcount = (this.Width - 16) / bit_spacing;
            int maxBits = Math.Max(64, GetDisplayBitCount());
            int bitstart = 0;
            int leftpos = 3;
            bool invalidate = false;

            if (bitcount < 32)
            {
                bitcount = 32;
            }

            if ((bitcount < maxBits) && (mPageNumber == 1))
                bitstart = maxBits - bitcount;

            if ((mLastReflowCount != bitcount) || (mLastReflowStart != bitstart))
            {
                invalidate = true;

                mLastReflowCount = bitcount;
                mLastReflowStart = bitstart;
            }

            for (int i = 0; i < 64; ++i)
            {
                if ((i < bitstart) || (bitcount == 0))
                    mBitTexts[i].Visible = false;
                else
                {
                    mBitTexts[i].Visible = true;
                    mBitTexts[i].Left = leftpos;
                    leftpos += bit_spacing;
                    bitcount--;
                }
            }

            // Refresh the 
            if (invalidate)
                Invalidate();
        }

        private void bitGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            Update_Bits();
            //Update_Grid();
            //this.Invalidate(true);

            mPendingSave = 5;
        }

        private void msbBit0_Click(object sender, EventArgs e)
        {
            if (msbBit0.Checked)
                mGridMode = GridMode.cIBM;
            else
                mGridMode = GridMode.cIntel;

            mUserMSBSwitch = true;
            Update(UpdateTypes.cNone);
            mUserMSBSwitch = false;
        }

        // swap the endianness of the data
        private void EndianSwap_Click(object sender, EventArgs e)
        {
            BigInteger oldValue = mBaseValue;

            mBaseValue = 0;
            for (int t = 0; t < 8; t++)
            {
                mBaseValue <<= 8;
                mBaseValue |= (oldValue & 0xFF);
                oldValue >>= 8;
            }
            Update(UpdateTypes.cBits);
            Update(UpdateTypes.cPage);
        }

        private void EndianSwapPage_Click(object sender, EventArgs e)
        {
            BigInteger oldValue = mBaseValue, shiftValue = mBaseValue;

            mBaseValue = 0;
            // high page 63:32
            if (mPageNumber == 0)
            {
                shiftValue >>= 32;
                for (int t = 0; t < 4; t++)
                {
                    mBaseValue <<= 8;
                    mBaseValue |= (shiftValue & 0xFF);
                    shiftValue >>= 8;
                }
                mBaseValue <<= 32;
                mBaseValue |= oldValue & 0xFFFFFFFF;
            }
            // low page 31:0
            else
            {
                mBaseValue = (oldValue >> 32) & 0xFFFFFFFF;
                for (int t = 0; t < 4; t++)
                {
                    mBaseValue <<= 8;
                    mBaseValue |= (oldValue & 0xFF);
                    oldValue >>= 8;
                }
            }
            Update(UpdateTypes.cBits);
            Update(UpdateTypes.cPage);
        }

        // Add this method to update the Save button's enabled state
        private void UpdateSaveButtonState()
        {
            if (predefBitsSaveButton != null)
                predefBitsSaveButton.Enabled = (mPendingSave > 0);
        }

        // Add this handler for the Save button
        private void predefBitsSaveButton_Click(object sender, EventArgs e)
        {
            mFileName = predefBitsCombo.Text + ".bits";
            Save_Grid();
            UpdateSaveButtonState();
        }

        private void label36_Click(object sender, EventArgs e)
        {
            mLeftBitCount = 64;

            Update_Labels();
            Update(UpdateTypes.cPage);
        }

        private void randButton_Click(object sender, EventArgs e)
        {
            mBaseValue = (BigInteger)(uint)mRandomGenerator.Next() << 32;
            mBaseValue |= (BigInteger)(uint)mRandomGenerator.Next();

            Update(UpdateTypes.cPage);
        }

        private void labelEdit_Leave(object sender, EventArgs e)
        {
            labelEdit.Visible = false;
            mComment[mLastLabelEdit.mCommentID] = labelEdit.Text.Trim(" \n\r".ToCharArray());
            Update_Label(mLastLabelEdit.mEditID);

            // A save of the comments is in order
            Save_Grid();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;

            if (labelEdit.Visible)
                labelEdit_Leave(sender, e);

            if ((me.Y >= 0) && (me.Y <= mBitTexts[0].Top))
            {
                foreach (BitTextExt bitbox in mBitTexts)
                {
                    if ((me.X >= bitbox.Left) && (me.X <= bitbox.Left + bitbox.Width) && bitbox.Visible)
                        Form1_BitLabelMouseClick(bitbox, me);
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (mBitTexts != null)
            {
                ReflowBits();
            }
        }

        private int GetDisplayBitCount()
        {
            if (mBaseValue == 0)
                return 1;

            return (int)mBaseValue.GetBitLength();
        }

        private int GetMaxVisibleBitIndex()
        {
            return Math.Max(63, GetDisplayBitCount() - 1);
        }
    }
}

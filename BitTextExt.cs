using System;
using System.Drawing;
using System.Windows.Forms;

namespace BitCalc64
{
    public class BitTextExt : UserControl
    {
        public delegate void RowSelectionHandler(Object sender, RowEvent re);

        private SizeF mMeasure0;
        private SizeF mMeasure1;
        private String mText;
        private String mLabelText;
        private BitPosition mPosition;

        public event RowSelectionHandler RowSelected;

        public ContentAlignment TextAlign { get; set; }

        public enum BitPosition
        {
            cNone,
            cLeft,
            cMiddle,
            cRight,
            cOrphan,
        };

        //public HorizontalAlignment TextAlign { get; set; }
        public override String Text { get { return mText; } set { mText = value; OnTextChanged(new EventArgs()); } }
        public String LabelText { get { return mLabelText; } set { mLabelText = value; this.Invalidate(); } }
        public BitPosition Position { get { return mPosition; } set { mPosition = value; Invalidate(); } }
        public RowId Row { get; set; }
        public Int32 Id { get; set; }

        // So the class knows who it is related to
        public BitTextExt Next { get; set; }
        public BitTextExt Prev { get; set; }

        public BitTextExt()
        {
            Text = "0";
            LabelText = "";
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Y < this.Height - 24)
            {
                base.OnMouseClick(e);
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (!this.Focused)
                    this.Focus();
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (Text == "0")
                    Text = "1";
                else
                    Text = "0";
            }
        }

        protected override void OnClick(EventArgs e)
        {
            if (!this.Focused)
                this.Focus();

            base.OnClick(e);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            this.Invalidate();
            RowSelected?.Invoke(this, new RowEvent(Id, Row));
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            this.Invalidate();
            base.OnLostFocus(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == '0')
            {
                Text = "0";
            }
            else if (e.KeyChar == '1')
            {
                Text = "1";
            }
            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            this.Invalidate();
            base.OnTextChanged(e);
        }

        private void SetToolTip()
        {
            string tiptext = "";
            switch (Position)
            {
                case BitPosition.cLeft:
                    tiptext = GetNext(true);
                    break;
                case BitPosition.cRight:
                    tiptext = GetPrev();
                    break;
                case BitPosition.cMiddle:
                    tiptext = GetPrev() + GetNext(false);
                    break;
                case BitPosition.cNone:
                    tiptext = Text;
                    break;
                case BitPosition.cOrphan:
                    tiptext = Text;
                    break;
            }
            return;
        }

        protected string GetNext(bool self)
        {
            string bits = (self) ? Text : "";
            if ((Next != null) && (Position != BitPosition.cRight))
                bits += Next.GetNext(true);

            return bits;
        }

        protected string GetPrev()
        {
            string bits = Text;
            if ((Prev != null) && (Position != BitPosition.cLeft))
                bits = Prev.GetPrev() + bits;

            return bits;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            Graphics g = this.CreateGraphics();

            mMeasure0 = g.MeasureString("0", Font);
            mMeasure1 = g.MeasureString("1", Font);

            base.OnFontChanged(e);
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            // Increase the width after we've been added to the parent
            if (!this.DesignMode)
            {
                this.Left -= 2;
                this.Width += 4;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            Rectangle r = pevent.ClipRectangle;
            Color back;
            int focus = Focused ? 0x30 : 0;
            Pen fp = Pens.Black.Clone() as Pen;

            // Clear the interior of the text, but not the labels
            pevent.Graphics.FillRectangle(new SolidBrush(this.Parent.BackColor), r);

            if (r.Height > 22)
            {
                r.Y = r.Height - 22;
                r.Height = 22;
            }

            // Store the new clip rect for drawing the focus box
            Rectangle cliprect = r;

            back = Color.FromArgb(Math.Max(BackColor.R - focus, 0), Math.Max(BackColor.G - focus, 0), Math.Max(BackColor.B - focus, 0));
            fp.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            // Draw the background
            switch (Position)
            {
                case BitPosition.cMiddle:
                    pevent.Graphics.FillRectangle(new SolidBrush(back), r);
                    pevent.Graphics.DrawLine(Pens.Black, r.Left, r.Top, r.Left + r.Width, r.Top);
                    pevent.Graphics.DrawLine(fp, r.Left, r.Top, r.Left, r.Top + r.Bottom);
                    pevent.Graphics.DrawLine(fp, r.Left + r.Width, r.Top, r.Left + r.Width, r.Top + r.Bottom);
                    pevent.Graphics.DrawLine(Pens.Black, r.Left, r.Bottom - 1, r.Left + r.Width, r.Bottom - 1);
                    break;

                case BitPosition.cLeft:
                    r.X += 2;
                    r.Width -= 2;
                    pevent.Graphics.FillRectangle(new SolidBrush(back), r);
                    pevent.Graphics.DrawLine(Pens.Black, r.Left, r.Top, r.Left, r.Top + r.Height);
                    pevent.Graphics.DrawLine(Pens.Black, r.Left, r.Top, r.Left + r.Width, r.Top);
                    pevent.Graphics.DrawLine(fp, r.Left + r.Width, r.Top, r.Left + r.Width, r.Top + r.Bottom);
                    pevent.Graphics.DrawLine(Pens.Black, r.Left, r.Bottom - 1, r.Left + r.Width, r.Bottom - 1);
                    break;

                case BitPosition.cRight:
                    r.Width -= 2;
                    pevent.Graphics.FillRectangle(new SolidBrush(back), r);
                    pevent.Graphics.DrawLine(Pens.Black, r.Left + r.Width, r.Top, r.Left + r.Width, r.Top + r.Height);
                    pevent.Graphics.DrawLine(fp, r.Left, r.Top, r.Left, r.Top + r.Bottom);
                    pevent.Graphics.DrawLine(Pens.Black, r.Left, r.Top, r.Left + r.Width, r.Top);
                    pevent.Graphics.DrawLine(Pens.Black, r.Left, r.Bottom - 1, r.Left + r.Width, r.Bottom - 1);
                    break;

                case BitPosition.cOrphan:
                    r.X += 2;
                    r.Width -= 4;
                    r.Height -= 1;
                    pevent.Graphics.FillRectangle(new SolidBrush(back), r);
                    pevent.Graphics.DrawRectangle(Pens.Black, r);
                    break;

                default:
                    r.X += 2;
                    r.Width -= 4;
                    pevent.Graphics.FillRectangle(new SolidBrush(back), r);
                    break;
            }

            // Draw the focus rectangle
            if (this.Focused)
            {
                fp.Width = 1;
                r = cliprect;
                fp.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                r.X += 2;
                r.Y += 1;
                r.Width -= 4;
                r.Height -= 3;
                pevent.Graphics.DrawRectangle(fp, r);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Paint the binary number
            float x = (this.Right - this.Left) / 2;
            float y = (this.Bottom - this.Top - 22 / 2);
            SizeF m = (Text == "0") ? mMeasure0 : mMeasure1;

            // Measure 
            x -= (m.Width / 2);
            y -= (m.Height / 2);

            e.Graphics.DrawString(Text, Font, Brushes.Black, x, y);

            // Paint the label
            //PaintLabel(e);
        }

        public void DrawLabel(Graphics g)
        {
            string[] lines = this.LabelText.Split("\n".ToCharArray());
            bool span_label = false;
            float xbase = this.Left - 1;
            float ybase = 0;
            float x = 0, y = 0;
            Font bfs = new("Arial Narrow", this.Font.Size - 3f, FontStyle.Regular);
            Font mini_bfs = new("Arial Narrow", this.Font.Size - 4.2f, FontStyle.Regular);
            Font draw_font;

            // whether we can draw the label larger
            if ((Next != null) && (Next.LabelText.Split("\n".ToCharArray())[0].Length == 0))
                span_label = true;

            // draw each of the lines without wrapping
            if (lines.Length > 0)
            {
                for (int i = 0; i < lines.Length; ++i)
                {
                    string measure = lines[i];
                    if (measure.Length == 0)
                        measure = "A";

                    SizeF me = g.MeasureString(measure, bfs);
                    draw_font = bfs;
                    if (Math.Floor(me.Width) > this.Width + 2)
                    {
                        x = 0;

                        // draw using a smaller font
                        if (!span_label)
                            draw_font = mini_bfs;
                    }
                    else if (i > 1)
                        x = (this.Width / 2 - me.Width / 2);

                    g.DrawString(lines[i], draw_font, Brushes.Black, x + xbase, y + ybase);

                    y += me.Height;
                }

                // draw left-right sides of the edit
                if (lines[0].Length > 0)
                    g.DrawLine(Pens.CadetBlue, xbase, 0, xbase, y / 2);
            }

            bfs.Dispose();
            mini_bfs.Dispose();
        }

        private string GetBinaryValue(string bits)
        {
            ulong value = 0;
            for (int i = 0; i < bits.Length; ++i)
            {
                value <<= 1;
                if (!bits[i].Equals('0'))
                    value |= 1;
            }
            return "0x" + value.ToString("X");
        }
    }

    public class RowEvent : EventArgs
    {
        public RowId Row { get; private set; }
        public Int32 Id { get; private set; }
        public RowEvent(Int32 id, RowId row) { Id = id; Row = row; }
    };

    public class RowId
    {
        public Int32 Start { get; set; }
        public Int32 End { get; set; }
        public RowId(Int32 start, Int32 end) { Start = start; End = end; }
    }
}

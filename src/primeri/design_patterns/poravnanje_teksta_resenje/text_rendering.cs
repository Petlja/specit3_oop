using PoravnanjeTeksta;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PoravnanjeTeksta
{
    abstract class TextAligner
    {
        protected Graphics graphics;
        protected Font font;
        protected TextFormatFlags flags;
        protected int basicSpaceWidth;
        protected const int lineSpacing = 10;
        protected const int margin = 10;

        protected TextAligner(Font f)
        {
            font = f;
            flags = TextFormatFlags.NoPadding;
        }
        public static TextAligner Create(Font f, int alignmentType)
        {
            switch (alignmentType)
            {
                case 0: return new TextAlignerLeft(f);
                case 1: return new TextAlignerRight(f);
                case 2: return new TextAlignerCentered(f);
                case 3: return new TextAlignerFull(f);
                default: return null;
            }
        }
    
        public void DisplayText(Graphics gr, List<string> paragraphs, int yStart, Size size)
        {
            graphics = gr;
            int y = yStart;
            Size proposedSize = new Size(int.MaxValue, int.MaxValue);
            basicSpaceWidth = TextRenderer.MeasureText(graphics, " ", font, proposedSize, flags).Width;
            foreach (string paragraph in paragraphs)
            {
                string[] words = paragraph.Split();
                int w = margin - basicSpaceWidth, h = 0, iStart = 0;
                int remainingSpaceWidth = size.Width + basicSpaceWidth - 2 * margin;
                for (int iWord = 0; iWord < words.Length; iWord++)
                {
                    Size wordSize = TextRenderer.MeasureText(graphics, words[iWord], font, proposedSize, flags);
                    if (basicSpaceWidth + wordSize.Width <= remainingSpaceWidth)
                    {
                        remainingSpaceWidth -= basicSpaceWidth + wordSize.Width;
                        h = Math.Max(h, wordSize.Height);
                    }
                    else
                    {
                        DisplayTextLine(words, iStart, iWord, remainingSpaceWidth, y);
                        iStart = iWord;
                        y += h + lineSpacing;
                        remainingSpaceWidth = size.Width - 2 * margin - wordSize.Width;
                        h = wordSize.Height;
                    }
                }
                DisplayTextLine(words, iStart, words.Length, remainingSpaceWidth, y);
                y += h + lineSpacing;
            }
        }
        private void DisplayTextLine(string[] words, int iStart, int iEnd, int remainingSpaceWidth, int y)
        {
            Point currentPoint = new Point(LineIndent(remainingSpaceWidth), y);
            int adjustedSpaceWidth = WordSpacing(iEnd - iStart, remainingSpaceWidth);
            Size proposedSize = new Size(int.MaxValue, int.MaxValue);
            for (int i = iStart; i < iEnd; i++)
            {
                Size size = TextRenderer.MeasureText(graphics, words[i], font, proposedSize, flags);
                TextRenderer.DrawText(graphics, words[i], font, currentPoint, Color.Black, flags);
                currentPoint.X += size.Width + adjustedSpaceWidth;
            }
        }
        abstract protected int LineIndent(int remainingSpaceWidth);
        abstract protected int WordSpacing(int numWords, int remainingSpaceWidth);
    }

    internal class TextAlignerLeft : TextAligner
    {
        public TextAlignerLeft(Font f) : base(f) { }
        protected override int LineIndent(int remainingSpaceWidth)
        {
            return margin;
        }
        protected override int WordSpacing(int numWords, int remainingSpaceWidth)
        {
            return basicSpaceWidth;
        }
    }
    internal class TextAlignerRight : TextAligner
    {
        public TextAlignerRight(Font f) : base(f) { }
        protected override int LineIndent(int remainingSpaceWidth)
        {
            return margin + remainingSpaceWidth;
        }
        protected override int WordSpacing(int numWords, int remainingSpaceWidth)
        {
            return basicSpaceWidth;
        }
    }
    internal class TextAlignerCentered : TextAligner
    {
        public TextAlignerCentered(Font f) : base(f) { }
        protected override int LineIndent(int remainingSpaceWidth)
        {
            return margin + remainingSpaceWidth / 2;
        }
        protected override int WordSpacing(int numWords, int remainingSpaceWidth)
        {
            return basicSpaceWidth;
        }
    }
    internal class TextAlignerFull : TextAligner
    {
        public TextAlignerFull(Font f) : base(f) { }
        protected override int LineIndent(int remainingSpaceWidth)
        {
            return margin;
        }
        protected override int WordSpacing(int numWords, int remainingSpaceWidth)
        {
            return numWords < 2 ?
                0 : basicSpaceWidth + remainingSpaceWidth / (numWords - 1);
        }
    }
}

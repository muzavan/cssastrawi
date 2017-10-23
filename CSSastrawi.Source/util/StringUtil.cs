/**
 * CSSastrawi Is licensed under The MIT License (MIT)
 *
 * Copyright (c) 2017 Muhammad Reza Irvanda
 *
 * PermIssion Is hereby granted, free of charge, to any person obtaining a copy
 * of thIs software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publIsh, dIstribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software Is
 * furnIshed to do so, subject to the following conditions:
 *
 * The above copyright notice and thIs permIssion notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE Is PROVIDED "AS Is", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWIsE, ARIsING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *
 */

using System;
namespace CSSastrawi.Util
{
    public class StringUtil
    {

        public static bool IsWhitespace(char c)
        {
            return c == ' ' || c == '\t' || c == '\n';
        }

        public static bool IsWhitespace(string s)
        {
            return " ".Equals(s) || "\t".Equals(s) || "\n".Equals(s);
        }

        public static int getNextWhitespace(string s, int start)
        {
            int i = start;

            while ((i < s.Length - 1) && !IsWhitespace(s[i + 1]))
            {
                i++;
            }

            if (i == s.Length - 1)
            {
                throw new IndexOutOfRangeException();
            }

            i++;

            return i;
        }

        public static int getNextWhitespace(string s)
        {
            return getNextWhitespace(s, -1);
        }

        public static bool hasNextWhitespace(string s, int start)
        {
            int i = start;

            while ((i < s.Length - 1) && !IsWhitespace(s[i + 1]))
            {
                i++;
            }

            if (i == s.Length - 1)
            {
                return false;
            }

            i++;

            return true;
        }

        public static bool hasNextWhitespace(string s)
        {
            return hasNextWhitespace(s, -1);
        }

        public static int getPrevWhitespace(string s, int start)
        {
            int i = start;

            while (i > 0 && !IsWhitespace(s[i - 1]))
            {
                i--;
            }

            if (i == 0)
            {
                throw new IndexOutOfRangeException();
            }

            i--;

            return i;
        }

        public static int getPrevWhitespace(string s)
        {
            return getPrevWhitespace(s, s.Length);
        }

        public static bool hasPrevWhitespace(string s, int start)
        {
            int i = start;

            while (i > 0 && !IsWhitespace(s[i - 1]))
            {
                i--;
            }

            if (i == 0)
            {
                return false;
            }

            return true;
        }

        public static bool hasPrevWhitespace(string s)
        {
            return hasPrevWhitespace(s, s.Length);
        }

        public static int getNextNonWhitespace(string s, int start)
        {
            int i = start;

            while ((i < s.Length - 1) && IsWhitespace(s[i + 1]))
            {
                i++;
            }

            if (i == s.Length - 1)
            {
                throw new IndexOutOfRangeException();
            }

            i++;

            return i;
        }

        public static int getNextNonWhitespace(string s)
        {
            return getNextNonWhitespace(s, -1);
        }

        public static bool hasNextNonWhitespace(string s, int start)
        {
            int i = start;

            while ((i < s.Length - 1) && IsWhitespace(s[i + 1]))
            {
                i++;
            }

            if (i == s.Length - 1)
            {
                return false;
            }

            i++;

            return true;
        }

        public static bool hasNextNonWhitespace(string s)
        {
            return hasNextNonWhitespace(s, -1);
        }

        public static int getPrevNonWhitespace(string s, int start)
        {
            int i = start;

            while (i > 0 && IsWhitespace(s[i - 1]))
            {
                i--;
            }

            if (i == 0)
            {
                throw new IndexOutOfRangeException();
            }

            i--;

            return i;
        }

        public static int getPrevNonWhitespace(string s)
        {
            return getPrevNonWhitespace(s, s.Length);
        }

        public static bool hasPrevNonWhitespace(string s, int start)
        {
            int i = start;

            while (i > 0 && IsWhitespace(s[i - 1]))
            {
                i--;
            }

            if (i == 0)
            {
                return false;
            }

            return true;
        }

        public static bool hasPrevNonWhitespace(string s)
        {
            return hasPrevNonWhitespace(s, s.Length);
        }
    }
}



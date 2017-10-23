
using System.Collections.Generic;
/**
* JSastrawi is licensed under The MIT License (MIT)
*
* Copyright (c) 2017 Muhammad Reza Irvanda
*
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*
*/
namespace CSSastrawi.Tokenization.Analyzer
{
    public class Alphanumeric : Analyzer
    {

        private readonly static ISet<char> alphanumericChars;

        static Alphanumeric()
        {
            string tmp = "abcdefghijklmnopqrstuvwxyz1234567890";

            alphanumericChars = new HashSet<char>();
            foreach (var c in tmp.ToCharArray())
            {
                alphanumericChars.Add(c);
            }
            foreach (var c in tmp.ToUpper().ToCharArray())
            {
                alphanumericChars.Add(c);
            }
        }

        static bool IsAlphanumeric(char c)
        {
            return alphanumericChars.Contains(c);
        }

        static bool prevCharIsDash(Model m)
        {
            if (m.GetPrevChar() == '-')
            {
                int prev2 = m.CharPos - 2;

                if (prev2 > 0 && m.Text[prev2] == '-')
                {
                    return true;
                }
            }

            return false;
        }

        public Analysis Analyze(Model m)
        {
            if (IsAlphanumeric(m.CurrentChar)
                    && (m.HasPrevChar() && m.CurrentChar == m.GetPrevChar())
                    || (m.HasPrevChar() && m.GetPrevChar() == '-')
                    && !prevCharIsDash(m))
            {
                return Analysis.SHOULD_NOT_SPLIT;
            }
            else if (IsAlphanumeric(m.CurrentChar)
                  && (m.HasPrevChar() && !IsAlphanumeric(m.GetPrevChar())))
            {
                return Analysis.SHOULD_SPLIT;
            }
            else if (IsAlphanumeric(m.CurrentChar) && !m.HasPrevChar())
            {
                return Analysis.SHOULD_SPLIT;
            }

            return Analysis.SKIP;
        }

    }
}


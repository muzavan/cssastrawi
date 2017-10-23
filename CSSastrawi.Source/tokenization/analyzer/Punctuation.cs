
using CSSastrawi.Tokenization.Analyzer;
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
    public class Punctuation : Analyzer
    {

        private static readonly ISet<char> punctuations;

        static Punctuation()
        {
            punctuations = new HashSet<char>();
            char[] tmp = ",.;:?!\"()\'[]+=*&^%$#@~`{}\\|><".ToCharArray();

            foreach (var c in tmp)
            {
                punctuations.Add(c);
            }
        }

        public Analysis Analyze(Model m)
        {
            if (IsPunctuation(m.CurrentChar)
                    && m.HasPrevChar() && m.CurrentChar == m.GetPrevChar())
            {
                return Analysis.SHOULD_NOT_SPLIT;
            }
            else if (IsPunctuation(m.CurrentChar))
            {
                return Analysis.SHOULD_SPLIT;
            }

            return Analysis.SKIP;
        }

        static bool IsPunctuation(char c)
        {
            return punctuations.Contains(c);
        }

    }

}


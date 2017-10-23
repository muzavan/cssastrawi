
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
    public class Hyphen : Analyzer
    {
        private readonly static ISet<char> alphabetChars;
        
        static Hyphen()
        {
            string tmp = "abcdefghijklmnopqrstuvwxyz";
            alphabetChars = new HashSet<char>();

            foreach(var c in tmp.ToCharArray()) {
                alphabetChars.Add(c);
            }
            foreach(var c in tmp.ToUpper().ToCharArray()) {
                alphabetChars.Add(c);
            }
        }

        public Analysis Analyze(Model m)
        {
            if (m.CurrentChar == '-')
            {
                // don't split dash
                if (m.HasNextChar() && m.GetNextChar() == '-')
                {
                    return Analysis.SHOULD_SPLIT;
                }
                else if (m.HasPrevChar() && IsAlphabet(m.GetPrevChar())
                      && m.HasNextChar() && IsAlphabet(m.GetNextChar()))
                {
                    return Analysis.SHOULD_NOT_SPLIT;
                }
            }

            return Analysis.SKIP;
        }

        static bool IsAlphabet(char c)
        {
            return alphabetChars.Contains(c);
        }
    }
}


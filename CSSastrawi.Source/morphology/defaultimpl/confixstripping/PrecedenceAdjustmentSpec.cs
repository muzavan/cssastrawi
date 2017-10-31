/**
* CSSastrawi is licensed under The MIT License (MIT)
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
using CSSastrawi.Specification;
using System.Text.RegularExpressions;

namespace Sastrawi.Mrphology.Defaultimpl.Confixstripping
{
        /**
         * Precedence Adjustment Specification as in confix stripping algorithm.
         * 
         * <p>
         * Asian J. 2007. Effective Techniques for Indonesian Text Retrieval. page 78.
         * </p>
         */
        public class PrecedenceAdjustmentSpec: Specification<string> {

        private string[] regexRules = {
            "^be(.*)lah$",
            "^be(.*)an$",
            "^me(.*)i$",
            "^di(.*)i$",
            "^pe(.*)i$",
            "^ter(.*)i$"
        };

        public bool IsSatisfiedBy(string word)
        {
            foreach (var rule in regexRules)
            {
                var regex = new Regex(rule);
                if (regex.Match(word).Success)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

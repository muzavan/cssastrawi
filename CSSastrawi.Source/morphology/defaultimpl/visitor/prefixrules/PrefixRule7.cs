
using System.Text.RegularExpressions;
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
namespace CSSastrawi.Morphology.Defaultimpl.Visitor.Prefixrules
{
    /**
     * Disambiguate Prefix Rule 7 : terCerv -&gt; ter-CerV where C != 'r'
     */
    public class PrefixRule7 : Disambiguator
    {
        const string prefixRule = "^ter([bcdfghjklmnpqrstvwxyz])er([aiueo].*)$";
        public string Disambiguate(string word)
        {
            var rule = new Regex(prefixRule, RegexOptions.Compiled);
            var match = rule.Match(word);
            if (match.Success)
            {
                var groups = match.Groups;
                if (groups[0].Value.Equals("r"))
                {
                    return word;
                }
                return groups[0].Value +"er"+groups[1].Value;
            }
            return word;
        }
    }

}
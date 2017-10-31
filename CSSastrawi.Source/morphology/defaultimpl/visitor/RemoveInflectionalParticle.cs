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
using CSSastrawi.Morphology.Defaultimpl;
using System.Text.RegularExpressions;

namespace CSSastrawi.Morphology.Defaultimpl.Visitor
{

    /**
     * Remove Inflectional Particle (lah|kah|tah|pun)
     */
    public class RemoveInflectionalParticle : ContextVisitor
    {
        public void Visit(Context context)
        {
            var result = Remove(context.CurrentWord);

            if (!result.Equals(context.CurrentWord))
            {
                var removedPart = context.CurrentWord.Replace(result, ""); // Replace first?

                Removal r = new RemovalImpl(this, context.CurrentWord, result, removedPart, "P");
                context.AddRemoval(r);
                context.CurrentWord = result;
            }
        }

        /**
         * Remove inflectional particle from a word
         *
         * @param word word
         * @return word after the derivational prefix has been removed
         */
        public string Remove(string word)
        {
            var regex = new Regex("(lah|kah|tah|pun)$");
            return regex.Replace(word,"");
        }

    }
}


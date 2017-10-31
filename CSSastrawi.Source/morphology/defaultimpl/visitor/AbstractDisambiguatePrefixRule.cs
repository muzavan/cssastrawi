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
using System.Collections.Generic;
namespace CSSastrawi.Morphology.Defaultimpl.Visitor
{
    /**
 * Base class to disambiguate prefix rules
 */
    public abstract class AbstractDisambiguatePrefixRule : ContextVisitor
    {

        protected List<Disambiguator> disambiguators = new List<Disambiguator>();

        public void Visit(Context context)
        {
            string result = null;

            foreach (var disambiguator in disambiguators)
            {
                result = disambiguator.Disambiguate(context.CurrentWord);

                if (context.Dictionary.Contains(result))
                {
                    break;
                }
            }

            if (null == result || result.Equals(context.CurrentWord))
            {
                return;
            }

            var removedPart = context.CurrentWord.Replace(result, "");
            var removal = new RemovalImpl(this, context.CurrentWord, result, removedPart, "DP");
            context.AddRemoval(removal);
            context.CurrentWord = result;
        }

        /**
         * Add disambiguator
         * 
         * @param disambiguator disambiguator
         */
        public void AddDisambiguator(Disambiguator disambiguator)
        {
            disambiguators.Add(disambiguator);
        }

        /**
         * Add many disambiguators at once
         * 
         * @param disambiguators a collection of disambiguators
         */
        public void AddDisambiguators(IEnumerable<Disambiguator> disambiguators)
        {
            foreach (var disambiguator in disambiguators)
            {
                this.disambiguators.Add(disambiguator);
            }
        }
    }
}



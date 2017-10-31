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
using CSSastrawi.Morphology.Defaultimpl.Visitor;

namespace CSSastrawi.Morphology.Defaultimpl
{
    /**
 * Standard implementation of Removal
 */
    public class RemovalImpl : Removal
    {

        private readonly ContextVisitor visitor;

        private readonly string subject;

        private readonly string result;

        private readonly string removedPart;

        private readonly string affixType;

        /**
         * Constructor
         *
         * @param visitor visitor
         * @param subject subject
         * @param result result
         * @param removedPart removed part
         * @param affixType affix type
         */
        public RemovalImpl(ContextVisitor visitor, string subject, string result, string removedPart, string affixType)
        {
            this.visitor = visitor;
            this.subject = subject;
            this.result = result;
            this.removedPart = removedPart;
            this.affixType = affixType;
        }


        public ContextVisitor GetVisitor()
        {
            return visitor;
        }


        public string GetSubject()
        {
            return subject;
        }


        public string GetResult()
        {
            return result;
        }


        public string GetRemovedPart()
        {
            return removedPart;
        }


        public string GetAffixType()
        {
            return affixType;
        }

    }

}



using CSSastrawi.Morphology.Defaultimpl.Visitor;
using Sastrawi.Mrphology.Defaultimpl.Confixstripping;
using System.Collections.Generic;
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
namespace CSSastrawi.Morphology.Defaultimpl
{
    /**
 * Encapsulated context in lemmatizing a word
 */
    public class Context
    {

        private readonly string originalWord;
        private string currentWord;
        private readonly ISet<string> dictionary;
        private readonly VisitorProvider visitorProvider;
        private List<Removal> removals;
        private string result;
        private readonly List<ContextVisitor> visitors;
        private readonly List<ContextVisitor> suffixVisitors;
        private readonly List<ContextVisitor> prefixVisitors;
        private bool processIsStopped;

        /**
         * Constructor
         *
         * @param originalWord original word
         * @param dictionary dictionary of root words
         * @param visitorProvider visitor provider
         */
        public Context(string originalWord, ISet<string> dictionary, VisitorProvider visitorProvider)
        {
            this.originalWord = originalWord;
            this.currentWord = this.originalWord;
            this.dictionary = dictionary;
            this.visitorProvider = visitorProvider;
            this.removals = new List<Removal>();

            this.visitors = visitorProvider.getVisitors();
            this.suffixVisitors = visitorProvider.getSuffixVisitors();
            this.prefixVisitors = visitorProvider.getPrefixVisitors();
        }

        /**
         * Get original word of which to find the lemma
         *
         * @return original word
         */
        public string OriginalWord
        {
            get
            {
                return originalWord;
            }
        }

        /**
         * Set the current state of the word in the lemmatization process
         *
         * @param currentWord current word
         */
        public string CurrentWord
        {
            get
            {
                return currentWord;
            }
            set
            {
                currentWord = value;
            }
        }
        
        /**
         * Add removal to the collection for later use
         *
         * @param r removal
         */
        public void AddRemoval(Removal r)
        {
            removals.Add(r);
        }

        /**
         * Get removals
         *
         * @return removals
         */
        public List<Removal> Removals
        {
            get{
                return removals;
            }
        }

        /**
         * Get the lemma as a result of the lemmatization process
         *
         * @return result
         */
        public string Result
        {
            get
            {
                return result;
            }
        }

        /**
         * Execute lemmatization process
         */
        public void Execute()
        {
            // step 1 - 5
            _StartStemmingProcess();

            // step 6
            if (dictionary.Contains(currentWord))
            {
                result = currentWord;
            }
            else
            {
                result = originalWord;
            }
        }

        private void _StartStemmingProcess()
        {

            // step 1
            if (dictionary.Contains(currentWord))
            {
                return;
            }

            if (currentWord.Length <= 3)
            {
                return;
            }

            acceptVisitors(visitors);

            if (dictionary.Contains(currentWord))
            {
                return;
            }

            var spec = new PrecedenceAdjustmentSpec();

            /*
             * Confix Stripping
             * Try to remove prefix before suffix if the specification is met
             */
            if (spec.IsSatisfiedBy(originalWord))
            {
                // step 4, 5
                _RemovePrefixes();
                if (dictionary.Contains(currentWord))
                {
                    return;
                }

                // step 2, 3
                _RemoveSuffixes();
                if (dictionary.Contains(currentWord))
                {
                    return;
                }
                else
                {
                    // if the trial is failed, restore the original word
                    // and continue to normal rule precedence (suffix first, prefix afterwards)
                    CurrentWord = originalWord;
                    removals.Clear();
                }
            }

            // step 2, 3
            _RemoveSuffixes();
            if (dictionary.Contains(currentWord))
            {
                return;
            }

            // step 4, 5
            _RemovePrefixes();
            if (dictionary.Contains(currentWord))
            {
                return;
            }

            _LoopPengembalianAkhiran();
        }

        private string acceptVisitors(List<ContextVisitor> visitors)
        {
            foreach(ContextVisitor visitor in visitors)
            {
                _Accept(visitor);

                if (dictionary.Contains(currentWord))
                {
                    return currentWord;
                }

                if (processIsStopped)
                {
                    return currentWord;
                }
            }

            return currentWord;
        }

        private void _Accept(ContextVisitor visitor)
        {
            visitor.Visit(this);
        }

        private void _RemoveSuffixes()
        {
            acceptVisitors(suffixVisitors);
        }

        private void _RemovePrefixes()
        {
            for (int i = 0; i < 3; i++)
            {
                _AcceptPrefixVisitors(prefixVisitors);
                if (dictionary.Contains(currentWord))
                {
                    return;
                }
            }
        }

        private void _AcceptPrefixVisitors(List<ContextVisitor> prefixVisitors)
        {
            int removalCount = removals.Count;

            foreach(var visitor in prefixVisitors)
            {
                _Accept(visitor);

                if (dictionary.Contains(currentWord))
                {
                    return;
                }

                if (processIsStopped)
                {
                    return;
                }

                if (removals.Count > removalCount)
                {
                    return;
                }
            }
        }

        /**
         * Get dictionary
         *
         * @return dictionary
         */
        public ISet<string> Dictionary
        {
            get
            {
                return dictionary;
            }
        }

        private void _LoopPengembalianAkhiran()
        {
            // restore prefix to form [DP+[DP+[DP]]] + Root word
            _RestorePrefix();

            List<Removal> originalRemovals = removals;
            List<Removal> reversedRemovals = new List<Removal>(removals);
            reversedRemovals.Reverse();
            string originalCurrentWord = currentWord;

            foreach(Removal removal in reversedRemovals)
            {
                if (!IsSuffixRemoval(removal))
                {
                    continue;
                }

                if (removal.GetRemovedPart().Equals("kan"))
                {
                    CurrentWord = removal.GetResult() + "k";

                    // step 4, 5
                    _RemovePrefixes();
                    if (dictionary.Contains(currentWord))
                    {
                        return;
                    }

                    CurrentWord = removal.GetResult() + "kan";
                }
                else
                {
                    CurrentWord = removal.GetSubject();
                }

                // step 4, 5
                _RemovePrefixes();
                if (dictionary.Contains(currentWord))
                {
                    return;
                }

                this.removals = originalRemovals;
                CurrentWord = originalCurrentWord;
            }
        }

        private void _RestorePrefix()
        {
            foreach(var removal in removals)
            {
                if (removal.GetAffixType().Equals("DP"))
                {
                    CurrentWord = removal.GetSubject();
                    break;
                }
            }

            removals.RemoveAll(x => x.GetAffixType().Equals("DP"));
        }

        private bool IsSuffixRemoval(Removal removal)
        {
            return removal.GetAffixType().Equals("DS")
                    || removal.GetAffixType().Equals("PP")
                    || removal.GetAffixType().Equals("P");
        }
    }
}



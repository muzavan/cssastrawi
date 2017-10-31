
using CSSastrawi.Morphology.Defaultimpl;
using CSSastrawi.Morphology.Defaultimpl.Visitor;
using System.Collections.Generic;
using System.Text.RegularExpressions;
/**
* CSSastrawi Is licensed under The MIT License (MIT)
*
* Copyright (c) 2017 Muhammad Reza Irvanda
*
* Permission Is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software Is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
*
* THE SOFTWARE Is PROVIDED "AS Is", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*
*/
namespace CSSastrawi.Morphology
{

    /**
     * The default implementation of Lemmatizer for Bahasa Indonesia.
     *
     * It implements several stemming algorithms:
     * <ul>
     * <li>Nazief and Adriani</li>
     * <li>Confix Stripping (CS)</li>
     * <li>Enhanced Confix Stripping (ECS)</li>
     * <li>Improved ECS</li>
     * </ul>
     *
     * Sastrawi also makes several improvements to the algorithms. Resources and
     * links to the original paper: <a
     * href="https://github.com/sastrawi/sastrawi/wiki/Resources">
     * https://github.com/sastrawi/sastrawi/wiki/Resources</a>.
     */
    public class DefaultLemmatizer : Lemmatizer
    {

        private readonly ISet<string> dictionary;
        private readonly VisitorProvider visitorProvider;

        /**
         * Construct the default lemmatizer. The algorithms really depends on a
         * dictionary of root words.
         *
         * @param dictionary dictionary of root words (base form)
         */
        public DefaultLemmatizer(ISet<string> dictionary)
        {
            this.dictionary = dictionary;
            this.visitorProvider = new VisitorProvider();
        }


        public string Lemmatize(string word)
        {
            word = word.ToLower();

            if (_IsPlural(word))
            {
                return _LemmatizePluralWord(word);
            }
            else
            {
                return _LemmatizeSingularWord(word);
            }
        }

        /**
         * @return the dictionary
         */
        public ISet<string> Dictionary
        {
            get
            {
                return dictionary;
            }
        }

        private bool _IsPlural(string word)
        {
            // -ku|-mu|-nya
            // nikmat-Ku, etc
            var regex = new Regex("^(.*)-(ku|mu|nya)$", RegexOptions.Compiled);
            var matcher = regex.Match(word);
            if (matcher.Success)
            {
                var groups = matcher.Groups;
                return groups[0].Value.Contains("-");
            }

            return word.Contains("-");
        }

        private string _LemmatizePluralWord(string word)
        {
            var regex = new Regex("^(.*)-(.*)$", RegexOptions.Compiled);
            var matcher = regex.Match(word);
            
            if (!matcher.Success)
            {
                return word;
            }
            var groups = matcher.Groups;
            string word1 = groups[0].Value;
            string word2 = groups[1].Value;

            if (string.IsNullOrEmpty(word1) || string.IsNullOrEmpty(word2))
            {
                return word;
            }

            // malaikat-malaikat-nya -> malaikat malaikat-nya
            string suffix = word2;
            var matcher2 = regex.Match(word1);

            var suffixRegex = new Regex("^ku|mu|nya$");
            if (suffixRegex.Match(suffix).Success && matcher2.Success)
            {
                var mgroups = matcher2.Groups;
                word1 = mgroups[0].Value;
                word2 = mgroups[1].Value + "-" + suffix;
            }

            // berbalas-balasan -> balas
            string lemma1 = _LemmatizeSingularWord(word1);
            string lemma2 = _LemmatizeSingularWord(word2);

            // meniru-nirukan -> tiru
            if (!dictionary.Contains(word2) && lemma2.Equals(word2))
            {
                lemma2 = _LemmatizeSingularWord("me" + word2);
            }

            if (lemma1.Equals(lemma2))
            {
                return lemma1;
            }
            else
            {
                return word;
            }
        }

        private string _LemmatizeSingularWord(string word)
        {
            Context context = new Context(word, dictionary, visitorProvider);
            context.Execute();

            return context.Result;
        }

    }
}


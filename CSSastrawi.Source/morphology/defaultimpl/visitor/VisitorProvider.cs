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
using CSSastrawi.Morphology.Defaultimpl.Visitor.Prefixrules;
using System.Collections.Generic;

namespace CSSastrawi.Morphology.Defaultimpl.Visitor
{
    /**
    * Context Visitor Provider. It provides visitors that will visit the context to
    * apply each rule.
    */
    public class VisitorProvider
    {

        private readonly List<ContextVisitor> visitors;
        private readonly List<ContextVisitor> suffixVisitors;
        private readonly List<ContextVisitor> prefixVisitors;

        /**
         * Constructor
         */
        public VisitorProvider()
        {
            visitors = new List<ContextVisitor>();

            suffixVisitors = new List<ContextVisitor>();
            suffixVisitors.Add(new RemoveInflectionalParticle()); // lah|kah|tah|pun
            suffixVisitors.Add(new RemoveInflectionalPossessivePronoun()); // ku|mu|nya
            suffixVisitors.Add(new RemoveDerivationalSuffix()); // i|kan|an

            prefixVisitors = new List<ContextVisitor>();

            prefixVisitors.Add(new RemovePlainPrefix()); // di|ke|se
            prefixVisitors.Add(new PrefixDisambiguator(new Disambiguator[]{
            new PrefixRule1a(),
            new PrefixRule1b(),}));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule2()));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule3()));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule4()));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule5()));
            prefixVisitors.Add(new PrefixDisambiguator(new Disambiguator[]{
            new PrefixRule6a(),
            new PrefixRule6b()
        }));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule7()));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule8()));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule9()));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule10()));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule11()));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule12()));
            prefixVisitors.Add(new PrefixDisambiguator(new Disambiguator[]{
            new PrefixRule13a(),
            new PrefixRule13b()
        }));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule14()));
            prefixVisitors.Add(new PrefixDisambiguator(new Disambiguator[]{
            new PrefixRule15a(),
            new PrefixRule15b()
        }));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule16()));
            prefixVisitors.Add(new PrefixDisambiguator(new Disambiguator[]{
            new PrefixRule17a(),
            new PrefixRule17b(),
            new PrefixRule17c(),
            new PrefixRule17d()
        }));

            prefixVisitors.Add(new PrefixDisambiguator(new Disambiguator[]{
            new PrefixRule18a(),
            new PrefixRule18b()
        }));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule19()));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule20()));
            prefixVisitors.Add(new PrefixDisambiguator(new Disambiguator[]{
            new PrefixRule21a(),
            new PrefixRule21b()
        }));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule23()));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule24()));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule25()));

            prefixVisitors.Add(new PrefixDisambiguator(new Disambiguator[]{
            new PrefixRule26a(),
            new PrefixRule26b()
        }));

            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule27()));
            prefixVisitors.Add(new PrefixDisambiguator(new Disambiguator[]{
            new PrefixRule28a(),
            new PrefixRule28b()
        }));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule29()));
            prefixVisitors.Add(new PrefixDisambiguator(new Disambiguator[]{
            new PrefixRule30a(),
            new PrefixRule30b()
        }));
            prefixVisitors.Add(new PrefixDisambiguator(new Disambiguator[]{
            new PrefixRule31a(),
            new PrefixRule31b()
        }));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule32()));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule34()));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule35()));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule36()));
            prefixVisitors.Add(new PrefixDisambiguator(new Disambiguator[]{
            new PrefixRule37a(),
            new PrefixRule37b()
        }));
            prefixVisitors.Add(new PrefixDisambiguator(new Disambiguator[]{
            new PrefixRule38a(),
            new PrefixRule38b()
        }));
            prefixVisitors.Add(new PrefixDisambiguator(new Disambiguator[]{
            new PrefixRule39a(),
            new PrefixRule39b()
        }));
            prefixVisitors.Add(new PrefixDisambiguator(new Disambiguator[]{
            new PrefixRule40a(),
            new PrefixRule40b()
        }));

            // Sastrawi Additional rules
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule41()));
            prefixVisitors.Add(new PrefixDisambiguator(new PrefixRule42()));
        }

        /**
         * Get suffix visitors
         *
         * @return suffix visitors
         */
        public List<ContextVisitor> SuffixVisitors
        {
            get
            {
                return suffixVisitors;
            }
        }

        /**
         * Get visitors
         *
         * @return visitors
         */
        public List<ContextVisitor> Visitors
        {
            get
            {
                return visitors;
            }

        }

        /**
         * Get prefix visitors
         *
         * @return prefix visitors
         */
        public List<ContextVisitor> PrefixVisitors
        {
            get
            {
                return prefixVisitors;
            }

        }
    }
}



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
using System.Text;
using CSSastrawi.Tokenization.Analyzer;
using System.Collections.Generic;

namespace CSSastrawi.Tokenization
{
    public class HeuristicTokenizer : Tokenizer
    {

        private readonly List<Analyzer.Analyzer> analyzers;
        
        public HeuristicTokenizer()
        {

            analyzers = new List<Analyzer.Analyzer>();
            analyzers.Add(new Alphanumeric());
            analyzers.Add(new Whitespace());
            analyzers.Add(new Punctuation());
            analyzers.Add(new Hyphen());
        }

        public List<Analyzer.Analyzer> Analyzers
        {
            get
            {
                return analyzers;
            }

        }

        public void AddAnalyzer(Analyzer.Analyzer a)
        {
            analyzers.Add(a);
        }

        public string[] Tokenize(string text)
        {
            List<string> tokens = new List<string>();
            StringBuilder tokenBuffer = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                Model m = new Model(text, i);

                int analysisSkip = 0;
                int analysisShoulSplit = 0;
                int analysisShouldNotSplit = 0;

                foreach (var analyzer in analyzers)
                {
                    Analysis analysis = analyzer.Analyze(m);

                    if (analysis == Analysis.SKIP)
                    {
                        analysisSkip++;
                    }
                    else if (analysis == Analysis.SHOULD_SPLIT)
                    {
                        analysisShoulSplit++;
                    }
                    else if (analysis == Analysis.SHOULD_NOT_SPLIT)
                    {
                        analysisShouldNotSplit++;
                    }
                }

                if (analysisShoulSplit > 0 && analysisShoulSplit >= analysisShouldNotSplit)
                {
                    if (m.CurrentChar != ' ')
                    {
                        if (tokenBuffer.Length > 0)
                        {
                            tokens.Add(tokenBuffer.ToString());
                            tokenBuffer = new StringBuilder();
                        }

                        tokenBuffer.Append(m.CurrentChar);
                    }
                    else
                    {
                        if (tokenBuffer.Length > 0)
                        {
                            tokens.Add(tokenBuffer.ToString());
                            tokenBuffer = new StringBuilder();
                        }
                    }
                }
                else
                {
                    tokenBuffer.Append(m.CurrentChar);
                }
            }

            if (tokenBuffer.Length > 0)
            {
                tokens.Add(tokenBuffer.ToString());
            }

            return tokens.ToArray();
        }

    }
}



using CSSastrawi.Cli.Output;
using CSSastrawi.Morphology;
using System.Collections.Generic;
using System.IO;
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
namespace CSSastrawi.Cli
{

    /**
     * Handler of lemmatize command
     */
    public class LemmatizeCmd
    {

        private readonly Output.Output output;

        /**
         * Constructor
         *
         * @param output Output.Output object. It is used to print messages.
         */
        public LemmatizeCmd(Output.Output output)
        {
            this.output = output;
        }

        /**
         * Constructor
         */
        public LemmatizeCmd()
        {
            this.output = new SystemOutput();
        }

        /**
         * @return output object.
         */
        public Output.Output Output
        {
            get
            {
                return output;
            }
        }

        /**
         * Handle lemmatize command
         *
         * @param args arguments
         * @throws IOException IOException
         */
        public void handle(string[] args)
        {

        }


        private void buildOptions()
        {

        }

        private ISet<string> GetDictionaryFromFile(string file)
        {
            ISet<string> dictionary = new HashSet<string>();
            var lines = File.ReadAllLines(file);
            _FillSet(dictionary, lines);

            return dictionary;
        }

        private ISet<string> GetDefaultDictionary()
        {
            ISet<string> dictionary = new HashSet<string>();
            var lines = File.ReadAllLines("/root-words.txt");
            _FillSet(dictionary, lines);
            return dictionary;
        }

        private void _FillSet(ISet<string> set, string[] lines)
        {
            foreach (var line in lines)
            {
                set.Add(line);
            }
        }

        void _RunTestBed(Dictionary<string, string> testbed, Lemmatizer lemmatizer)
        {
            int successCount = 0;
            int failedCount = 0;
            float successRate = 0;
            Dictionary<string, string> failures = new Dictionary<string, string>();
            Dictionary<string, string> actuals = new Dictionary<string, string>();

            foreach (var entry in testbed)
            {
                string lemma = lemmatizer.Lemmatize(entry.Key);
                if (lemma.Equals(entry.Value))
                {
                    successCount++;
                }
                else
                {
                    failedCount++;
                    failures[entry.Key] = entry.Value;
                    actuals[entry.Key] = lemma;
                }
            }

            if (testbed.Count > 0)
            {
                successRate = (float)successCount * 100 / testbed.Count;
            }

            output.WriteLine("Total test : " + testbed.Count);
            output.WriteLine("Success : " + successCount);
            output.WriteLine("Failed : " + failedCount);
            output.WriteLine("Success rate : " + successRate + "%");

            if (failedCount > 0)
            {
                output.WriteLine("Failures:");
                int idx = 0;
                foreach (var entry in failures)
                {

                    output.WriteLine("[" + idx + "] word: " + entry.Key + ", expected: " + entry.Value + ", actual: " + actuals[entry.Key]);
                    idx++;
                }
            }
        }

        private Dictionary<string, string> _ScanTestBedDictionaryFromFile(string filePath)
        {
            var map = new Dictionary<string, string>();
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var splitted = line.Split(',');
                if (splitted.Length >= 2)
                {
                    map[splitted[0]] = splitted[1];
                }
            }

            return map;
        }
    }
}
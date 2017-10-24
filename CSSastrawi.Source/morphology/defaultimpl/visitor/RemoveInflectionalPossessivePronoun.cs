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
package CSSastrawi.morphology.defaultimpl.visitor;

import CSSastrawi.morphology.defaultimpl.Context;
import CSSastrawi.morphology.defaultimpl.Removal;
import CSSastrawi.morphology.defaultimpl.RemovalImpl;

/**
 * Remove Inflectional Possessive Pronoun (ku|mu|nya)
 */
class RemoveInflectionalPossessivePronoun implements ContextVisitor {

    @Override
    public void visit(Context context) {
        String result = remove(context.getCurrentWord());

        if (!result.equals(context.getCurrentWord())) {
            String removedPart = context.getCurrentWord().replaceFirst(result, "");

            Removal r = new RemovalImpl(this, context.getCurrentWord(), result, removedPart, "PP");
            context.addRemoval(r);
            context.setCurrentWord(result);
        }
    }

    /**
     * Remove inflectional possessive pronoun from a word
     *
     * @param word word
     * @return word after the possessive pronoun has been removed
     */
    public String remove(String word) {
        return word.replaceAll("-*(ku|mu|nya)$", "");
    }

}

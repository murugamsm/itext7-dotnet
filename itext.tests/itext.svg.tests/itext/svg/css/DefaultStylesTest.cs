/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
Authors: iText Software.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using System.Collections.Generic;
using System.IO;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;
using iText.Svg;
using iText.Svg.Css.Impl;
using iText.Svg.Dummy.Sdk;
using iText.Test;

namespace iText.Svg.Css {
    public class DefaultStylesTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void CheckDefaultStrokeValuesTest() {
            ICssResolver styleResolver = new SvgStyleResolver();
            iText.StyledXmlParser.Jsoup.Nodes.Element svg = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf("svg"), "");
            INode svgNode = new JsoupElementNode(svg);
            IDictionary<String, String> resolvedStyles = styleResolver.ResolveStyles(svgNode, null);
            NUnit.Framework.Assert.AreEqual("1", resolvedStyles.Get(SvgConstants.Attributes.STROKE_OPACITY));
            NUnit.Framework.Assert.AreEqual("1px", resolvedStyles.Get(SvgConstants.Attributes.STROKE_WIDTH));
            NUnit.Framework.Assert.AreEqual(SvgConstants.Values.NONE, resolvedStyles.Get(SvgConstants.Attributes.STROKE
                ));
            NUnit.Framework.Assert.AreEqual(SvgConstants.Values.BUTT, resolvedStyles.Get(SvgConstants.Attributes.STROKE_LINECAP
                ));
            NUnit.Framework.Assert.AreEqual("0", resolvedStyles.Get(SvgConstants.Attributes.STROKE_DASHOFFSET));
            NUnit.Framework.Assert.AreEqual(SvgConstants.Values.NONE, resolvedStyles.Get(SvgConstants.Attributes.STROKE_DASHARRAY
                ));
            NUnit.Framework.Assert.AreEqual("4", resolvedStyles.Get(SvgConstants.Attributes.STROKE_MITERLIMIT));
        }

        [NUnit.Framework.Test]
        public virtual void CheckDefaultFillValuesTest() {
            ICssResolver styleResolver = new SvgStyleResolver();
            iText.StyledXmlParser.Jsoup.Nodes.Element svg = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf("svg"), "");
            INode svgNode = new JsoupElementNode(svg);
            IDictionary<String, String> resolvedStyles = styleResolver.ResolveStyles(svgNode, null);
            NUnit.Framework.Assert.AreEqual("black", resolvedStyles.Get(SvgConstants.Attributes.FILL));
            NUnit.Framework.Assert.AreEqual(SvgConstants.Values.FILL_RULE_NONZERO, resolvedStyles.Get(SvgConstants.Attributes
                .FILL_RULE));
            NUnit.Framework.Assert.AreEqual("1", resolvedStyles.Get(SvgConstants.Attributes.FILL_OPACITY));
        }

        [NUnit.Framework.Test]
        public virtual void CheckDefaultFontValuesTest() {
            ICssResolver styleResolver = new SvgStyleResolver();
            iText.StyledXmlParser.Jsoup.Nodes.Element svg = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf("svg"), "");
            INode svgNode = new JsoupElementNode(svg);
            IDictionary<String, String> resolvedStyles = styleResolver.ResolveStyles(svgNode, null);
            NUnit.Framework.Assert.AreEqual("helvetica", resolvedStyles.Get(SvgConstants.Attributes.FONT_FAMILY));
            NUnit.Framework.Assert.AreEqual("12px", resolvedStyles.Get(SvgConstants.Attributes.FONT_SIZE));
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStreamTest() {
            ICssResolver styleResolver = new SvgStyleResolver(new MemoryStream(new byte[] {  }));
            iText.StyledXmlParser.Jsoup.Nodes.Element svg = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf("svg"), "");
            INode svgNode = new JsoupElementNode(svg);
            IDictionary<String, String> resolvedStyles = styleResolver.ResolveStyles(svgNode, null);
            NUnit.Framework.Assert.AreEqual(0, resolvedStyles.Count);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStylesFallbackTest() {
            NUnit.Framework.Assert.That(() =>  {
                new SvgStyleResolver(new ExceptionInputStream());
            }
            , NUnit.Framework.Throws.InstanceOf<System.IO.IOException>())
;
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-2289")]
        public virtual void InheritedDefaultStyleTest() {
            ICssResolver styleResolver = new SvgStyleResolver();
            iText.StyledXmlParser.Jsoup.Nodes.Element svg = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf("svg"), "");
            iText.StyledXmlParser.Jsoup.Nodes.Element circle = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf("circle"), "");
            INode svgNode = new JsoupElementNode(svg);
            svgNode.AddChild(new JsoupElementNode(circle));
            IDictionary<String, String> resolvedStyles = styleResolver.ResolveStyles(svgNode.ChildNodes()[0], null);
            NUnit.Framework.Assert.AreEqual("black", resolvedStyles.Get(SvgConstants.Attributes.STROKE));
        }
    }
}

using System;
using System.Linq.Expressions;
using NUnit.Framework;
using Test.Urasandesu.NAnonym.Etc;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Test;
using Urasandesu.NTroll.FormulaSample.Mixins.System.Linq.Expressions;
using Assert = Urasandesu.NAnonym.Test.Assert;

namespace Test.Urasandesu.NTroll.FormulaSample.Mixins.System.Linq.Expressions
{
    [TestFixture]
    public class ExpressionMixinTest
    {
        [Test]
        public void ToFormulaTest01()
        {
            var writeLog = typeof(TestHelper).GetMethod("WriteLog", new Type[] { typeof(string), typeof(object[]) });
            Assert.AreEqual(
                "{" + 
                    "\"NodeType\": \"Call\", " + 
                    "\"Type\": \"System.Void\", " + 
                    "\"Instance\": null, " + 
                    "\"Method\": \"Void WriteLog(System.String, System.Object[])\", " + 
                    "\"Arguments\": " + 
                    "[" + 
                        "{" + 
                            "\"NodeType\": \"Constant\", " + 
                            "\"Type\": \"System.String\", " + 
                            "\"Value\": \"testtest\"" + 
                        "}, " + 
                        "{" + 
                            "\"NodeType\": \"NewArrayInit\", " + 
                            "\"Type\": \"Urasandesu.NTroll.FormulaSample.Formulas.Formula[]\", " + 
                            "\"Formulas\": []" + 
                        "}" + 
                    "]" + 
                "}",
                Dump(() => writeLog.Invoke(null, new object[] { "testtest", new object[] { } }))
            );

            var p1 = default(PropertyTestClass1);
            var p1Ci = typeof(PropertyTestClass1).GetConstructor(Type.EmptyTypes);
            Assert.AreEqual(
                "{" + 
                    "\"NodeType\": \"Assign\", " + 
                    "\"Type\": \"Test.Urasandesu.NAnonym.Etc.PropertyTestClass1\", " + 
                    "\"Left\": " + 
                    "{" + 
                        "\"NodeType\": \"Variable\", " + 
                        "\"Type\": \"Test.Urasandesu.NAnonym.Etc.PropertyTestClass1\", " + 
                        "\"Name\": \"p1\"" + 
                    "}, " + 
                    "\"Method\": \"=\", " + 
                    "\"Right\": " + 
                    "{" + 
                        "\"NodeType\": \"Convert\", " + 
                        "\"Type\": \"Test.Urasandesu.NAnonym.Etc.PropertyTestClass1\", " + 
                        "\"Method\": \"null\", " + 
                        "\"Operand\": " + 
                        "{" + 
                            "\"NodeType\": \"New\", " + 
                            "\"Type\": \"Test.Urasandesu.NAnonym.Etc.PropertyTestClass1\", " + 
                            "\"Constructor\": \"Void .ctor()\", " + 
                            "\"Parameters\": null, " + 
                            "\"Members\": null" + 
                        "}" + 
                    "}" + 
                "}", 
                Dump(() => Dsl.Allocate(p1).As((PropertyTestClass1)p1Ci.Invoke(null)))
            );
            Assert.AreEqual(
                "{" + 
                    "\"NodeType\": \"Call\", " + 
                    "\"Type\": \"System.Void\", " + 
                    "\"Instance\": null, " + 
                    "\"Method\": \"Void WriteLog(System.String, System.Object[])\", " + 
                    "\"Arguments\": " + 
                    "[" + 
                        "{" + 
                            "\"NodeType\": \"Constant\", " + 
                            "\"Type\": \"System.String\", " + 
                            "\"Value\": \"{0}\"" + 
                        "}, " + 
                        "{" + 
                            "\"NodeType\": \"NewArrayInit\", " + 
                            "\"Type\": \"Urasandesu.NTroll.FormulaSample.Formulas.Formula[]\", " + 
                            "\"Formulas\": " + 
                            "[" + 
                                "{" + 
                                    "\"NodeType\": \"Variable\", " + 
                                    "\"Type\": \"Test.Urasandesu.NAnonym.Etc.PropertyTestClass1\", " + 
                                    "\"Name\": \"p1\"" + 
                                "}" + 
                            "]" + 
                        "}" + 
                    "]" + 
                "}", 
                Dump(() => writeLog.Invoke(null, new object[] { "{0}", new object[] { p1 } }))
            );

            var p1ValueProperty = typeof(PropertyTestClass1).GetProperty("ValueProperty");
            Assert.AreEqual(
                "{" + 
                    "\"NodeType\": \"Assign\", " + 
                    "\"Type\": \"System.Int32\", " + 
                    "\"Left\": " + 
                    "{" + 
                        "\"NodeType\": \"Property\", " + 
                        "\"Type\": \"System.Int32\", " + 
                        "\"Instance\": " + 
                        "{" + 
                            "\"NodeType\": \"Variable\", " + 
                            "\"Type\": \"Test.Urasandesu.NAnonym.Etc.PropertyTestClass1\", " + 
                            "\"Name\": \"p1\"" + 
                        "}, " + 
                        "\"Member\": \"Int32 ValueProperty\"" + 
                    "}, " + 
                    "\"Method\": \"=\", " + 
                    "\"Right\": " + 
                    "{" + 
                        "\"NodeType\": \"Convert\", " + 
                        "\"Type\": \"System.Object\", " + 
                        "\"Method\": \"null\", " + 
                        "\"Operand\": " + 
                        "{" + 
                            "\"NodeType\": \"Constant\", " + 
                            "\"Type\": \"System.Int32\", " + 
                            "\"Value\": \"10\"" + 
                        "}" + 
                    "}" + 
                "}", 
                Dump(() => p1ValueProperty.SetValue(p1, 10, null))
            );
            Assert.AreEqual(
                "{" + 
                    "\"NodeType\": \"Call\", " + 
                    "\"Type\": \"System.Void\", " + 
                    "\"Instance\": null, " + 
                    "\"Method\": \"Void WriteLog(System.String, System.Object[])\", " + 
                    "\"Arguments\": " + 
                    "[" + 
                        "{" + 
                            "\"NodeType\": \"Constant\", " + 
                            "\"Type\": \"System.String\", " + 
                            "\"Value\": \"ValueProperty: {0}\"" + 
                        "}, " + 
                        "{" + 
                            "\"NodeType\": \"NewArrayInit\", " + 
                            "\"Type\": \"Urasandesu.NTroll.FormulaSample.Formulas.Formula[]\", " + 
                            "\"Formulas\": " + 
                            "[" + 
                                "{" + 
                                    "\"NodeType\": \"Convert\", " + 
                                    "\"Type\": \"System.Object\", " + 
                                    "\"Method\": \"null\", " + 
                                    "\"Operand\": " + 
                                    "{" + 
                                        "\"NodeType\": \"Convert\", " + 
                                        "\"Type\": \"System.Int32\", " + 
                                        "\"Method\": \"null\", " + 
                                        "\"Operand\": " + 
                                        "{" + 
                                            "\"NodeType\": \"Property\", " + 
                                            "\"Type\": \"System.Int32\", " + 
                                            "\"Instance\": " + 
                                            "{" + 
                                                "\"NodeType\": \"Variable\", " + 
                                                "\"Type\": \"Test.Urasandesu.NAnonym.Etc.PropertyTestClass1\", " + 
                                                "\"Name\": \"p1\"" + 
                                            "}, " + 
                                            "\"Member\": \"Int32 ValueProperty\"" + 
                                        "}" + 
                                    "}" + 
                                "}" + 
                            "]" + 
                        "}" + 
                    "]" + 
                "}", 
                Dump(() => writeLog.Invoke(null, new object[] { "ValueProperty: {0}", new object[] { (int)p1ValueProperty.GetValue(p1, null) } }))
            );

            var p1ObjectProperty = typeof(PropertyTestClass1).GetProperty("ObjectProperty");
            Assert.AreEqual(
                "{" + 
                    "\"NodeType\": \"Assign\", " + 
                    "\"Type\": \"System.String\", " + 
                    "\"Left\": " + 
                    "{" + 
                        "\"NodeType\": \"Property\", " + 
                        "\"Type\": \"System.String\", " + 
                        "\"Instance\": " + 
                        "{" + 
                            "\"NodeType\": \"Variable\", " + 
                            "\"Type\": \"Test.Urasandesu.NAnonym.Etc.PropertyTestClass1\", " + 
                            "\"Name\": \"p1\"" + 
                        "}, " + 
                        "\"Member\": \"System.String ObjectProperty\"" + 
                    "}, " + 
                    "\"Method\": \"=\", " + 
                    "\"Right\": " + 
                    "{" + 
                        "\"NodeType\": \"Constant\", " + 
                        "\"Type\": \"System.String\", " + 
                        "\"Value\": \"a\"" + 
                    "}" + 
                "}", 
                Dump(() => p1ObjectProperty.SetValue(p1, "a", null))
            );
            Assert.AreEqual(
                "{" + 
                    "\"NodeType\": \"Assign\", " + 
                    "\"Type\": \"System.String\", " + 
                    "\"Left\": " + 
                    "{" + 
                        "\"NodeType\": \"Property\", " + 
                        "\"Type\": \"System.String\", " + 
                        "\"Instance\": " + 
                        "{" + 
                            "\"NodeType\": \"Variable\", " + 
                            "\"Type\": \"Test.Urasandesu.NAnonym.Etc.PropertyTestClass1\", " + 
                            "\"Name\": \"p1\"" + 
                        "}, " + 
                        "\"Member\": \"System.String ObjectProperty\"" + 
                    "}, " + 
                    "\"Method\": \"=\", " + 
                    "\"Right\": " + 
                    "{" + 
                        "\"NodeType\": \"Property\", " + 
                        "\"Type\": \"System.String\", " + 
                        "\"Instance\": " + 
                        "{" + 
                            "\"NodeType\": \"Variable\", " + 
                            "\"Type\": \"Test.Urasandesu.NAnonym.Etc.PropertyTestClass1\", " + 
                            "\"Name\": \"p1\"" + 
                        "}, " + 
                        "\"Member\": \"System.String ObjectProperty\"" + 
                    "}" + 
                "}", 
                Dump(() => p1ObjectProperty.SetValue(p1, p1ObjectProperty.GetValue(p1, null), null))
            );
            Assert.AreEqual(
                "{" + 
                    "\"NodeType\": \"Call\", " + 
                    "\"Type\": \"System.Void\", " + 
                    "\"Instance\": null, " + 
                    "\"Method\": \"Void WriteLog(System.String, System.Object[])\", " + 
                    "\"Arguments\": " + 
                    "[" + 
                        "{" + 
                            "\"NodeType\": \"Constant\", " + 
                            "\"Type\": \"System.String\", " + 
                            "\"Value\": \"ObjectProperty: {0}\"" + 
                        "}, " + 
                        "{" + 
                            "\"NodeType\": \"NewArrayInit\", " + 
                            "\"Type\": \"Urasandesu.NTroll.FormulaSample.Formulas.Formula[]\", " + 
                            "\"Formulas\": " + 
                            "[" + 
                                "{" + 
                                    "\"NodeType\": \"Property\", " + 
                                    "\"Type\": \"System.String\", " + 
                                    "\"Instance\": " + 
                                    "{" + 
                                        "\"NodeType\": \"Variable\", " + 
                                        "\"Type\": \"Test.Urasandesu.NAnonym.Etc.PropertyTestClass1\", " + 
                                        "\"Name\": \"p1\"" + 
                                    "}, " + 
                                    "\"Member\": \"System.String ObjectProperty\"" + 
                                "}" + 
                            "]" + 
                        "}" + 
                    "]" + 
                "}", 
                Dump(() => writeLog.Invoke(null, new object[] { "ObjectProperty: {0}", new object[] { p1ObjectProperty.GetValue(p1, null) } }))
            );

            var f2 = default(FieldTestClass2);
            Assert.AreEqual(
                "{" + 
                    "\"NodeType\": \"Assign\", " + 
                    "\"Type\": \"Test.Urasandesu.NAnonym.Etc.FieldTestClass2\", " + 
                    "\"Left\": " + 
                    "{" + 
                        "\"NodeType\": \"Variable\", " + 
                        "\"Type\": \"Test.Urasandesu.NAnonym.Etc.FieldTestClass2\", " + 
                        "\"Name\": \"f2\"" + 
                    "}, " + 
                    "\"Method\": \"=\", " + 
                    "\"Right\": " + 
                    "{" + 
                        "\"NodeType\": \"New\", " + 
                        "\"Type\": \"Test.Urasandesu.NAnonym.Etc.FieldTestClass2\", " + 
                        "\"Constructor\": \"Void .ctor()\", " + 
                        "\"Parameters\": [], " + 
                        "\"Members\": null" + 
                    "}" + 
                "}", 
                Dump(() => Dsl.Allocate(f2).As(new FieldTestClass2()))
            );
            var f2ValueField = typeof(FieldTestClass2).GetField("ValueField");
            Assert.AreEqual(
                "{" + 
                    "\"NodeType\": \"Assign\", " + 
                    "\"Type\": \"System.Int32\", " + 
                    "\"Left\": " + 
                    "{" + 
                        "\"NodeType\": \"Field\", " + 
                        "\"Type\": \"System.Int32\", " + 
                        "\"Instance\": " + 
                        "{" + 
                            "\"NodeType\": \"Variable\", " + 
                            "\"Type\": \"Test.Urasandesu.NAnonym.Etc.FieldTestClass2\", " + 
                            "\"Name\": \"f2\"" + 
                        "}, " + 
                        "\"Member\": \"Int32 ValueField\"" + 
                    "}, " + 
                    "\"Method\": \"=\", " + 
                    "\"Right\": " + 
                    "{" + 
                        "\"NodeType\": \"Convert\", " + 
                        "\"Type\": \"System.Object\", " + 
                        "\"Method\": \"null\", " + 
                        "\"Operand\": " + 
                        "{" + 
                            "\"NodeType\": \"Constant\", " + 
                            "\"Type\": \"System.Int32\", " + 
                            "\"Value\": \"30\"" + 
                        "}" + 
                    "}" + 
                "}", 
                Dump(() => f2ValueField.SetValue(f2, 30))
            );
            Assert.AreEqual(
                "{" + 
                    "\"NodeType\": \"Assign\", " + 
                    "\"Type\": \"System.Int32\", " + 
                    "\"Left\": " + 
                    "{" + 
                        "\"NodeType\": \"Field\", " + 
                        "\"Type\": \"System.Int32\", " + 
                        "\"Instance\": " + 
                        "{" + 
                            "\"NodeType\": \"Variable\", " + 
                            "\"Type\": \"Test.Urasandesu.NAnonym.Etc.FieldTestClass2\", " + 
                            "\"Name\": \"f2\"" + 
                        "}, " + 
                        "\"Member\": \"Int32 ValueField\"" + 
                    "}, " + 
                    "\"Method\": \"=\", " + 
                    "\"Right\": " + 
                    "{" + 
                        "\"NodeType\": \"Field\", " + 
                        "\"Type\": \"System.Int32\", " + 
                        "\"Instance\": " + 
                        "{" + 
                            "\"NodeType\": \"Variable\", " + 
                            "\"Type\": \"Test.Urasandesu.NAnonym.Etc.FieldTestClass2\", " + 
                            "\"Name\": \"f2\"" + 
                        "}, " + 
                        "\"Member\": \"Int32 ValueField\"" + 
                    "}" + 
                "}", 
                Dump(() => f2ValueField.SetValue(f2, f2ValueField.GetValue(f2)))
            );
            Assert.AreEqual(
                "{" + 
                    "\"NodeType\": \"Call\", " + 
                    "\"Type\": \"System.Void\", " + 
                    "\"Instance\": null, " + 
                    "\"Method\": \"Void WriteLog(System.String, System.Object[])\", " + 
                    "\"Arguments\": " + 
                    "[" + 
                        "{" + 
                            "\"NodeType\": \"Constant\", " + 
                            "\"Type\": \"System.String\", " + 
                            "\"Value\": \"ValueField: {0}\"" + 
                        "}, " + 
                        "{" + 
                            "\"NodeType\": \"NewArrayInit\", " + 
                            "\"Type\": \"Urasandesu.NTroll.FormulaSample.Formulas.Formula[]\", " + 
                            "\"Formulas\": " + 
                            "[" + 
                                "{" + 
                                    "\"NodeType\": \"Convert\", " + 
                                    "\"Type\": \"System.Object\", " + 
                                    "\"Method\": \"null\", " + 
                                    "\"Operand\": " + 
                                    "{" + 
                                        "\"NodeType\": \"Convert\", " + 
                                        "\"Type\": \"System.Int32\", " + 
                                        "\"Method\": \"null\", " + 
                                        "\"Operand\": " + 
                                        "{" + 
                                            "\"NodeType\": \"Field\", " + 
                                            "\"Type\": \"System.Int32\", " + 
                                            "\"Instance\": " + 
                                            "{" + 
                                                "\"NodeType\": \"Variable\", " + 
                                                "\"Type\": \"Test.Urasandesu.NAnonym.Etc.FieldTestClass2\", " + 
                                                "\"Name\": \"f2\"" + 
                                            "}, " + 
                                            "\"Member\": \"Int32 ValueField\"" + 
                                        "}" + 
                                    "}" + 
                                "}" + 
                            "]" + 
                        "}" + 
                    "]" + 
                "}", 
                Dump(() => TestHelper.WriteLog("ValueField: {0}", (int)f2ValueField.GetValue(f2)))
            );
            Assert.AreEqual(
                "{" + 
                    "\"NodeType\": \"Call\", " + 
                    "\"Type\": \"System.Void\", " + 
                    "\"Instance\": null, " + 
                    "\"Method\": \"Void WriteLog(System.String, System.Object[])\", " + 
                    "\"Arguments\": " + 
                    "[" + 
                        "{" + 
                            "\"NodeType\": \"Constant\", " + 
                            "\"Type\": \"System.String\", " + 
                            "\"Value\": \"ValueField: {0}\"" + 
                        "}, " + 
                        "{" + 
                            "\"NodeType\": \"NewArrayInit\", " + 
                            "\"Type\": \"Urasandesu.NTroll.FormulaSample.Formulas.Formula[]\", " + 
                            "\"Formulas\": " + 
                            "[" + 
                                "{" + 
                                    "\"NodeType\": \"Field\", " + 
                                    "\"Type\": \"System.Int32\", " + 
                                    "\"Instance\": " + 
                                    "{" + 
                                        "\"NodeType\": \"Variable\", " + 
                                        "\"Type\": \"Test.Urasandesu.NAnonym.Etc.FieldTestClass2\", " + 
                                        "\"Name\": \"f2\"" + 
                                    "}, " + 
                                    "\"Member\": \"Int32 ValueField\"" + 
                                "}" + 
                            "]" + 
                        "}" + 
                    "]" + 
                "}", 
                Dump(() => writeLog.Invoke(null, new object[] { "ValueField: {0}", new object[] { f2ValueField.GetValue(f2) } }))
            );

            var p2 = default(PropertyTestClass2);
            var p2Ci = typeof(PropertyTestClass2).GetConstructor(new Type[] { typeof(int), typeof(string) });
            Assert.AreEqual(
                "{" + 
                    "\"NodeType\": \"Assign\", " + 
                    "\"Type\": \"Test.Urasandesu.NAnonym.Etc.PropertyTestClass2\", " + 
                    "\"Left\": " + 
                    "{" + 
                        "\"NodeType\": \"Variable\", " + 
                        "\"Type\": \"Test.Urasandesu.NAnonym.Etc.PropertyTestClass2\", " + 
                        "\"Name\": \"p2\"" + 
                    "}, " + 
                    "\"Method\": \"=\", " + 
                    "\"Right\": " + 
                    "{" + 
                        "\"NodeType\": \"Convert\", " + 
                        "\"Type\": \"Test.Urasandesu.NAnonym.Etc.PropertyTestClass2\", " + 
                        "\"Method\": \"null\", " + 
                        "\"Operand\": " + 
                        "{" + 
                            "\"NodeType\": \"New\", " + 
                            "\"Type\": \"Test.Urasandesu.NAnonym.Etc.PropertyTestClass2\", " + 
                            "\"Constructor\": \"Void .ctor(Int32, System.String)\", " + 
                            "\"Parameters\": " + 
                            "[" + 
                                "{" + 
                                    "\"NodeType\": \"Property\", " + 
                                    "\"Type\": \"System.Int32\", " + 
                                    "\"Instance\": " + 
                                    "{" + 
                                        "\"NodeType\": \"Variable\", " + 
                                        "\"Type\": \"Test.Urasandesu.NAnonym.Etc.PropertyTestClass1\", " + 
                                        "\"Name\": \"p1\"" + 
                                    "}, " + 
                                    "\"Member\": \"Int32 ValueProperty\"" + 
                                "}, " + 
                                "{" + 
                                    "\"NodeType\": \"Property\", " + 
                                    "\"Type\": \"System.String\", " + 
                                    "\"Instance\": " + 
                                    "{" + 
                                        "\"NodeType\": \"Variable\", " + 
                                        "\"Type\": \"Test.Urasandesu.NAnonym.Etc.PropertyTestClass1\", " + 
                                        "\"Name\": \"p1\"" + 
                                    "}, " + 
                                    "\"Member\": \"System.String ObjectProperty\"" + 
                                "}" + 
                            "], " + 
                            "\"Members\": null" + 
                        "}" + 
                    "}" + 
                "}", 
                Dump(() => Dsl.Allocate(p2).As((PropertyTestClass2)p2Ci.Invoke(new object[] { p1ValueProperty.GetValue(p1, null), p1ObjectProperty.GetValue(p1, null) })))
            );
            Assert.AreEqual(
                "{" + 
                    "\"NodeType\": \"Call\", " + 
                    "\"Type\": \"System.Void\", " + 
                    "\"Instance\": null, " + 
                    "\"Method\": \"Void WriteLog(System.String, System.Object[])\", " + 
                    "\"Arguments\": " + 
                    "[" + 
                        "{" + 
                            "\"NodeType\": \"Constant\", " + 
                            "\"Type\": \"System.String\", " + 
                            "\"Value\": \"({0}, {1})\"" + 
                        "}, " + 
                        "{" + 
                            "\"NodeType\": \"NewArrayInit\", " + 
                            "\"Type\": \"Urasandesu.NTroll.FormulaSample.Formulas.Formula[]\", " + 
                            "\"Formulas\": " + 
                            "[" + 
                                "{" + 
                                    "\"NodeType\": \"Convert\", " + 
                                    "\"Type\": \"System.Object\", " + 
                                    "\"Method\": \"null\", " + 
                                    "\"Operand\": " + 
                                    "{" + 
                                        "\"NodeType\": \"Variable\", " + 
                                        "\"Type\": \"System.Int32\", " + 
                                        "\"Name\": \"ValueProperty\"" + 
                                    "}" + 
                                "}, " + 
                                "{" + 
                                    "\"NodeType\": \"Variable\", " + 
                                    "\"Type\": \"System.String\", " + 
                                    "\"Name\": \"ObjectProperty\"" + 
                                "}" + 
                            "]" + 
                        "}" +
                    "]" + 
                "}", 
                Dump(() => writeLog.Invoke(null, new object[] { "({0}, {1})", new object[] { p2.ValueProperty, p2.ObjectProperty } }))
            );

            var getValue = typeof(TestHelper).GetMethod("GetValue", new Type[] { typeof(int) });
            var value = default(int);
            Assert.AreEqual(
                "{" + 
                    "\"NodeType\": \"Assign\", " + 
                    "\"Type\": \"System.Int32\", " + 
                    "\"Left\": " + 
                    "{" + 
                        "\"NodeType\": \"Variable\", " + 
                        "\"Type\": \"System.Int32\", " + 
                        "\"Name\": \"value\"" + 
                    "}, " + 
                    "\"Method\": \"=\", " + 
                    "\"Right\": " + 
                    "{" + 
                        "\"NodeType\": \"Convert\", " + 
                        "\"Type\": \"System.Int32\", " + 
                        "\"Method\": \"null\", " + 
                        "\"Operand\": " + 
                        "{" + 
                            "\"NodeType\": \"Call\", " + 
                            "\"Type\": \"System.Int32\", " + 
                            "\"Instance\": null, " + 
                            "\"Method\": \"Int32 GetValue(Int32)\", " + 
                            "\"Arguments\": " + 
                            "[" + 
                                "{" + 
                                    "\"NodeType\": \"Field\", " + 
                                    "\"Type\": \"System.Int32\", " + 
                                    "\"Instance\": " + 
                                    "{" + 
                                        "\"NodeType\": \"Variable\", " + 
                                        "\"Type\": \"Test.Urasandesu.NAnonym.Etc.FieldTestClass2\", " + 
                                        "\"Name\": \"f2\"" + 
                                    "}, " + 
                                    "\"Member\": \"Int32 ValueField\"" + 
                                "}" + 
                            "]" + 
                        "}" + 
                    "}" + 
                "}", 
                Dump(() => Dsl.Allocate(value).As((int)getValue.Invoke(null, new object[] { f2ValueField.GetValue(f2) })))
            );
        }

        string Dump(Expression<Action> exp)
        {
            return exp.Body.ToFormula().ToString();
        }
    }
}

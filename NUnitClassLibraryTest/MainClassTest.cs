using System;
using NUnit.Framework;
using NUnitClassLibrary;
using System.IO;
using System.Collections;

namespace NUnitClassLibraryTest
{
    // TestFixture 属性: クラスに付ける
    [TestFixture]
    public class MainClassTest
    {
        #region Common run
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            // run once time before class test
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            // Add Dispose
            // run once time after class test
        }
        [SetUp]
        public static void SetUp()
        {
            // run when before running method test
        }
        [TearDown]
        public static void TearDown()
        {
            // run when after running method test 
        }
        #endregion

        #region No Attribute
        // 何も付けないと、テストしてくれない、
        // せめて[Test]をつけてほしい
        public void TestNoAttribute()
        { }
        #endregion

        #region Basic Test
        //Test 属性: テストメソッドに付ける
        [Test]
        public void TestBasic()
        {
            var testClass = new MainClass();
            var expectedValue = 100;
            var actualValue = testClass.method1();

            Assert.True(expectedValue == actualValue);

            Assert.AreEqual(expectedValue, actualValue);

            Assert.IsEmpty("");
            Assert.IsNotNull("");
            /*
		    * よく使う Assertion には、ほかに次のようなものがある。
		    * Equality Asserts: AreEqual(), AreNotEqual()
		    * Condition Asserts: IsFalse() (False()), IsNull() (Null()), IsNotNull() (NotNull()), IsEmpty()
		    */
        }
        #endregion

        #region TestCase
        static object[] TestCases =
        {
            // object{parm1、parm2、期待結果}
            new object[] {300, 200, true},
            new object[] {100, 200, false}
        };
        [TestCaseSource("TestCases")]
        public void TestTestCase(int parm1, int parm2, bool result)
        {
            var testClass = new MainClass();
            var actualValue = testClass.method2(parm1, parm2);

            Assert.AreEqual(result, actualValue);
        }
        #endregion

        #region StringAssert
        static object[] TestStringAssertCases =
        {
            // object{caseNo, expectedValue、actualValue}
            new object[] {1, "abc", "123abcde"},
            new object[] {2, "ABC", "123abcde"},
            new object[] {3, "abc", "abcde"},
            new object[] {4, "b", "abcde"},
            new object[] {5, "aBcＡ", "AbCａ"},
            // @"\d"は全角数字にもマッチする。注意!
            new object[] {6, @"^0[0-9]+-\d+-[0-9]{4}$", "052-99９-0000"}
        };
        [TestCaseSource("TestStringAssertCases")]
        public void TestStringAssert(int caseNo, string expectedValue, string actualValue)
        {
            switch (caseNo)
            {
                case 1:
                    StringAssert.Contains(expectedValue, actualValue);
                    break;
                case 2:
                    StringAssert.DoesNotContain(expectedValue, actualValue);
                    break;
                case 3:
                    StringAssert.StartsWith(expectedValue, actualValue);
                    break;
                case 4:
                    StringAssert.DoesNotStartWith(expectedValue, actualValue);
                    // 同様に、指定した文字列で終わっているかどうかを判定する EndsWith(), DoesNotEndWith() もある。
                    break;
                case 5:
                    StringAssert.AreEqualIgnoringCase(expectedValue, actualValue);
                    Assert.AreEqual(expectedValue.ToUpper(), actualValue.ToUpper());
                    break;
                case 6:
                    StringAssert.IsMatch(expectedValue, actualValue);
                    break;
            }

        }
        #endregion

        #region FileAssert
        static object[] FileAssertTestCases =
        {
            // object{path1、path2}
            new object[] {"./テストデータ.txt", "./テストデータ1.txt"}
        };
        [TestCaseSource("FileAssertTestCases")]
        public void TestFileAssert(string path1, string path2)
        {
            Assert.False(File.Exists(path1));

            // FileAssert.AreEqual(path1, path2) compare test in file
            // FileAssert.AreNotEqual(path1, path2) compare test in file
            //using (var stream1 = File.OpenRead(path1))
            //using (var stream2 = File.OpenRead(path2))
            //{
            //    FileAssert.AreEqual(stream1, stream2);
            //    FileAssert.AreNotEqual(stream1, stream2);
            //    FileAssert.AreEqual(stream1, stream2);
            //}
        }
        #endregion

        #region CollectionAssert
        static object[] CollectionAssertTestCases =
        {
            // object{calseNo, expectedValue、actualValue、期待結果}
            new object[] {1,new int[]{1, 2, 3,}, new int[] { 1, 2, 3, }},
            new object[] {2,new int[]{1, 3, 2,}, new int[] { 1, 2, 3, }},
            new object[] {3,new int[]{1, 2, },   new int[] { 1, 2, 3, }},
            new object[] {4,new int[] { 1, 2, 3, 3, },   new int[] { 3, 1, 3, 2, }},
            new object[] {5,new int[]{1, 2, },           new int[] { 3, 1, 3, 2, }},
            new object[] {6,new int[] { 1, 2, 2, 3, },   new int[] { 3, 1, 3, 2, }},
            new object[] {7,new int[] { 3, 1, },         new int[] { 3, 1, 3, 2, }},
            new object[] {8,new int[] { 3, 1, },         new int[] {}},
            new object[] {9,new int[] { 1, 2, 2, 3, },   new int[] { 3, 1, 3, 2, }},
            new object[] {10,new int[] { 1, 2, 2, 3, },  new int[] { 3, 1, 3, 2, }},
            new object[] {11,new int[] { 1, 2, 3, 4, },  new int[] { 3, 1, 4, 2, }},
            new object[] {12,new int[] { 1, 2, 3, 4, },  new int[] { 3, 4, 5, 6, }}
        };
        [TestCaseSource("CollectionAssertTestCases")]
        public void TestCollectionAssert(int caseNo, int[] expectedValue, int[] actualValue)
        {
            switch (caseNo)
            {
                case 1:
                    Assert.AreEqual(expectedValue, actualValue);    //順序も一致
                    break;
                case 2:
                    CollectionAssert.AreNotEqual(expectedValue, actualValue);	//順序が違えば等しくない
                    break;
                case 3:
                    CollectionAssert.AreNotEqual(expectedValue, actualValue);	//要素数が違っても等しくない
                    break;
                case 4:
                    CollectionAssert.AreEquivalent(expectedValue, actualValue);
                    break;
                case 5:
                    CollectionAssert.AreNotEquivalent(expectedValue, actualValue);	//要素数が違えば等しくない
                    break;
                case 6:
                    CollectionAssert.AreNotEquivalent(expectedValue, actualValue);
                    break;
                case 7:
                    CollectionAssert.Contains(actualValue, 2);	// 1要素のとき
                    CollectionAssert.IsSubsetOf(expectedValue, actualValue);	// 複数要素を確認したいとき(順序は問わない)
                    CollectionAssert.DoesNotContain(actualValue, 0);
                    CollectionAssert.IsNotSubsetOf(new int[] { 2, 4, }, actualValue);
                    break;
                case 8:
                    CollectionAssert.IsEmpty(actualValue);
                    CollectionAssert.IsNotEmpty(expectedValue);
                    break;
                case 9:
                    CollectionAssert.AllItemsAreNotNull(actualValue);
                    CollectionAssert.DoesNotContain(actualValue, null);

                    // null を含んでいることを検証するには Contains() を使う
                    //CollectionAssert.Contains(nullを含む配列, null);
                    break;
                case 10:
                    // 全ての要素が同じ型か
                    CollectionAssert.AllItemsAreInstancesOfType(actualValue, typeof(int));
                    break;
                case 11:
                    CollectionAssert.AllItemsAreUnique(actualValue);

                    //var 同じ値があるが型が違う = new object[] { 1, 2, 2.0, };
			        //CollectionAssert.AllItemsAreUnique(同じ値があるが型が違う);
			        // ※ 比較の際に暗黙の型変換が行われるため、これは失敗する
                    break;
                case 12:
                    CollectionAssert.IsOrdered(actualValue);
                    break;
            }

        }
        #endregion

        #region ExpectedException
        [Test]
        public void TestExpectedException()
        {
            var testClass = new MainClass();

            var ex = Assert.Throws<System.ApplicationException>(() => testClass.method3());

            StringAssert.StartsWith("アプリケーションでエラーが発生しました。", ex.Message);

            var innerEx = ex.InnerException;
            if (innerEx != null)
            {
                Assert.IsInstanceOf<NullReferenceException>(innerEx);
            }
        }
        #endregion

        #region AssertThat
        [Test]
        public void TestAssertThat()
        {
            // 文字列比較
            Assert.That("aBc", Is.Not.EqualTo("ABC"));
            Assert.That("aBc", Is.EqualTo("ABC").IgnoreCase);
            Assert.That("aBc", Is.EqualTo("ABC").Using((IComparer)StringComparer.OrdinalIgnoreCase));

            // 浮動小数点数
            Assert.That(99.9, Is.Not.EqualTo(100.0));
            Assert.That(99.9, Is.EqualTo(100.0).Within(1).Percent);	// 1%の許容誤差

            Assert.That("aBc", Has.Length.EqualTo(3) & Is.StringStarting("a") & Is.StringContaining("B"));
        }
        #endregion
    }
}

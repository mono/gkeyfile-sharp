//  
//  GKeyFileTests.cs
//  
//  Author:
//       Alex Launi <alex.launi@gmail.com>
// 
//  Copyright (c) 2010 Alex Launi
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 
using System;
using System.IO;
using System.Linq;

using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;

using KeyFile;

[TestFixture()]
public class GKeyFileTests
{
	const string TEST_FILE_NAME = "test-keyfile.keyfile";

	string test_file_name;
	GKeyFile test_keyfile;

	[SetUp]
	public void Init ()
	{
		test_file_name = new [] {Environment.CurrentDirectory, "..", "..", "src", TEST_FILE_NAME}.Aggregate (Path.Combine);
		test_keyfile = new GKeyFile (test_file_name);
	}	

	[Test]
	public void GetString ()
	{
		Assert.AreEqual ("astring", test_keyfile.GetString ("Group1", "String"));
		Assert.AreEqual ("a string with spaces", test_keyfile.GetString ("Group1", "StringWithSpaces"));
		Assert.AreEqual ("a\tstring\nwith escaping", test_keyfile.GetString ("Group1", "StringWithEscaping"));
	}

	[Test]
	public void GetBoolean ()
	{
		Assert.IsTrue (test_keyfile.GetBoolean ("Group1", "TrueBool"));
		Assert.IsFalse (test_keyfile.GetBoolean ("Group1", "FalseBool"));
	}

	[Test]
	public void GetInteger ()
	{
		Assert.AreEqual (256, test_keyfile.GetInteger ("Group1", "Integer"));
		Assert.AreEqual (-1024, test_keyfile.GetInteger ("Group1", "NegativeInt"));
	}

	[Test]
	public void GetDouble ()
	{
		Assert.AreEqual (11.28, test_keyfile.GetDouble ("Group1", "Double"));
		Assert.AreEqual (-28.11, test_keyfile.GetDouble ("Group1", "NegativeDouble"));
	}

	[Test]
	public void GetStringList ()
	{
		string [] expected = new string [] { "one", "two", "three", "four" };

		CollectionAssert.AreEqual (expected, test_keyfile.GetStringList ("Group1", "StringList"));
	}

	[Test]
	public void GetBooleanList ()
	{
		bool [] expected = new bool [] { true, false, false, true };

		CollectionAssert.AreEqual (expected, test_keyfile.GetBooleanList ("Group1", "BoolList"));
	}

	[Test]
	public void GetIntegerList ()
	{
		int [] expected = new int [] { 0, 1, 2, 04, -5 };

		CollectionAssert.AreEqual (expected, test_keyfile.GetIntegerList ("Group1", "IntegerList"));
	}

	[Test]
	public void GetDoubleList ()
	{
		double [] expected = new double [] { 10, 10.3, 46.3, -0.8 };

		CollectionAssert.AreEqual (expected, test_keyfile.GetDoubleList ("Group1", "DoubleList"));
	}

	[Test]
	public void GetNonstandardSeperatorStringList ()
	{
		GKeyFile keyFile = new GKeyFile (test_file_name);
		string [] expected = new string [] { "one one", "two", "threee", "for" };
		keyFile.ListSeparator = ":";

		CollectionAssert.AreEqual (expected, keyFile.GetStringList ("Group1", "NonSemicolonStringList"));
	}
}

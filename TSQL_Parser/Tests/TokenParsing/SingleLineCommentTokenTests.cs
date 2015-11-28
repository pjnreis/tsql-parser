﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TSQL;
using TSQL.Tokens;

namespace Tests.TokenParsing
{
	[TestFixture(Category = "Token Parsing")]
	public class SingleLineCommentTokenTests
	{
		[Test]
		public void SingleLineCommentToken_CarriageReturn()
		{
			List<TSQLToken> tokens = TSQLLexer.ParseTokens("--blah\r", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLSingleLineComment(0, "--blah"),
						new TSQLWhitespace(6, "\r")
					},
				tokens);
		}

		[Test]
		public void SingleLineCommentToken_LineFeed()
		{
			List<TSQLToken> tokens = TSQLLexer.ParseTokens("--blah\n", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLSingleLineComment(0, "--blah"),
						new TSQLWhitespace(6, "\n")
					},
				tokens);
		}
	}
}

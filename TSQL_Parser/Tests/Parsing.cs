﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using NUnit.Framework;

using TSQL;
using TSQL.Tokens;

using Tests.TokenParsing;

namespace Tests
{
	[TestFixture(Category = "Expression Parsing")]
	public class Parsing
	{
		[Test]
		public void Parse_GO()
		{
			List<TSQLToken> tokens = TSQLLexer.ParseTokens("GO");
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLKeyword(0, "GO")
					},
				tokens);
		}

		[Test]
		public void Parse_GOFromStream()
		{
			using (TextReader reader = new StringReader("GO"))
			using (TSQLLexer lexer = new TSQLLexer(reader))
			{
				TokenComparisons.CompareStreamToList(
					new List<TSQLToken>()
						{
							new TSQLKeyword(0, "GO")
						},
						lexer);
			}
		}

		[Test]
		public void Parse_uspSearchCandidateResumes_NoWhitespace()
		{
			using (StreamReader reader = new StreamReader("./Scripts/AdventureWorks2014.dbo.uspSearchCandidateResumes.sql"))
			using (TSQLLexer lexer = new TSQLLexer(reader))
			{
				TokenComparisons.CompareStreamStartToList(
					GetuspSearchCandidateResumesTokens()
						.Where(t => !(t is TSQLWhitespace)).ToList(),
					lexer);
			}
		}

		private List<TSQLToken> GetuspSearchCandidateResumesTokens()
		{
			return 
				new List<TSQLToken>()
					{
						new TSQLKeyword(0, "USE"),
						new TSQLWhitespace(3, " "),
						new TSQLIdentifier(4, "[AdventureWorks2014]"),
						new TSQLWhitespace(24, "\r\n"),
						new TSQLKeyword(26, "GO"),
						new TSQLWhitespace(28, "\r\n"),
						new TSQLMultilineComment(30 , "/****** Object:  StoredProcedure [dbo].[uspSearchCandidateResumes]    Script Date: 11/26/2015 9:21:50 PM ******/"),
						new TSQLWhitespace(142, "\r\n"),
						new TSQLKeyword(144, "SET"),
						new TSQLWhitespace(147, " "),
						new TSQLIdentifier(148, "ANSI_NULLS"),
						new TSQLWhitespace(158, " "),
						new TSQLKeyword(159, "ON"),
                        new TSQLWhitespace(161, "\r\n"),
						new TSQLKeyword(163, "GO"),
						new TSQLWhitespace(165, "\r\n"),
						new TSQLKeyword(167, "SET"),
						new TSQLWhitespace(170, " "),
						new TSQLIdentifier(171, "QUOTED_IDENTIFIER"),
						new TSQLWhitespace(188, " "),
						new TSQLKeyword(189, "ON"),
						new TSQLWhitespace(191, "\r\n"),
						new TSQLKeyword(193, "GO"),
						new TSQLWhitespace(195, "\r\n\r\n"),
						new TSQLSingleLineComment(199, "--A stored procedure which demonstrates integrated full text search"),
						new TSQLWhitespace(266, "\r\n\r\n"),
						new TSQLKeyword(270, "CREATE"),
						new TSQLWhitespace(276, " "),
						new TSQLKeyword(277, "PROCEDURE"),
						new TSQLWhitespace(286, " "),
						new TSQLIdentifier(287, "[dbo]"),
						new TSQLCharacter(292, "."),
					};
        }

		[Test]
		public void Parse_uspSearchCandidateResumes()
		{
			using (StreamReader reader = new StreamReader("./Scripts/AdventureWorks2014.dbo.uspSearchCandidateResumes.sql"))
			using (TSQLLexer lexer = new TSQLLexer(reader) { IncludeWhitespace = true })
			{
				TokenComparisons.CompareStreamStartToList(
					GetuspSearchCandidateResumesTokens(),
					lexer);
			}
		}


	}
}

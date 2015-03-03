using TUVienna.CS_CUP.Runtime;

class Yylex
{
	private const int YY_BUFFER_SIZE = 512;
	private const int YY_F = -1;
	private const int YY_NO_STATE = -1;
	private const int YY_NOT_ACCEPT = 0;
	private const int YY_START = 1;
	private const int YY_END = 2;
	private const int YY_NO_ANCHOR = 4;
	private const int YY_BOL = 128;
	private const int YY_EOF = 129;
	private System.IO.TextReader yy_reader;
	private int yy_buffer_index;
	private int yy_buffer_read;
	private int yy_buffer_start;
	private int yy_buffer_end;
	private char[] yy_buffer;
	private bool yy_at_bol;
	private int yy_lexical_state;

	public Yylex(System.IO.TextReader yy_reader1)
		: this()
	{
		if (null == yy_reader1)
		{
			throw (new System.Exception("Error: Bad input stream initializer."));
		}
		yy_reader = yy_reader1;
	}

	private Yylex()
	{
		yy_buffer = new char[YY_BUFFER_SIZE];
		yy_buffer_read = 0;
		yy_buffer_index = 0;
		yy_buffer_start = 0;
		yy_buffer_end = 0;
		yy_at_bol = true;
		yy_lexical_state = YYINITIAL;
	}

	private bool yy_eof_done = false;
	private const int YYINITIAL = 0;
	private static readonly int[] yy_state_dtrans = new int[] {
		0
	};
	private void yybegin(int state)
	{
		yy_lexical_state = state;
	}
	private int yy_advance()
	{
		int next_read;
		int i;
		int j;

		if (yy_buffer_index < yy_buffer_read)
		{
			return yy_buffer[yy_buffer_index++];
		}

		if (0 != yy_buffer_start)
		{
			i = yy_buffer_start;
			j = 0;
			while (i < yy_buffer_read)
			{
				yy_buffer[j] = yy_buffer[i];
				++i;
				++j;
			}
			yy_buffer_end = yy_buffer_end - yy_buffer_start;
			yy_buffer_start = 0;
			yy_buffer_read = j;
			yy_buffer_index = j;
			next_read = yy_reader.Read(yy_buffer,
					yy_buffer_read,
					yy_buffer.Length - yy_buffer_read);
			if (next_read <= 0)
			{
				return YY_EOF;
			}
			yy_buffer_read = yy_buffer_read + next_read;
		}

		while (yy_buffer_index >= yy_buffer_read)
		{
			if (yy_buffer_index >= yy_buffer.Length)
			{
				yy_buffer = yy_double(yy_buffer);
			}
			next_read = yy_reader.Read(yy_buffer,
					yy_buffer_read,
					yy_buffer.Length - yy_buffer_read);
			if (next_read <= 0)
			{
				return YY_EOF;
			}
			yy_buffer_read = yy_buffer_read + next_read;
		}
		return yy_buffer[yy_buffer_index++];
	}
	private void yy_move_end()
	{
		if (yy_buffer_end > yy_buffer_start &&
			'\n' == yy_buffer[yy_buffer_end - 1])
			yy_buffer_end--;
		if (yy_buffer_end > yy_buffer_start &&
			'\r' == yy_buffer[yy_buffer_end - 1])
			yy_buffer_end--;
	}
	private bool yy_last_was_cr = false;
	private void yy_mark_start()
	{
		yy_buffer_start = yy_buffer_index;
	}
	private void yy_mark_end()
	{
		yy_buffer_end = yy_buffer_index;
	}
	private void yy_to_mark()
	{
		yy_buffer_index = yy_buffer_end;
		yy_at_bol = (yy_buffer_end > yy_buffer_start) &&
					('\r' == yy_buffer[yy_buffer_end - 1] ||
					 '\n' == yy_buffer[yy_buffer_end - 1] ||
					 2028/*LS*/ == yy_buffer[yy_buffer_end - 1] ||
					 2029/*PS*/ == yy_buffer[yy_buffer_end - 1]);
	}
	private string yytext()
	{
		return (new string(yy_buffer,
			yy_buffer_start,
			yy_buffer_end - yy_buffer_start));
	}
	private int yylength()
	{
		return yy_buffer_end - yy_buffer_start;
	}
	private char[] yy_double(char[] buf)
	{
		int i;
		char[] newbuf;
		newbuf = new char[2 * buf.Length];
		for (i = 0; i < buf.Length; ++i)
		{
			newbuf[i] = buf[i];
		}
		return newbuf;
	}
	private const int YY_E_INTERNAL = 0;
	private const int YY_E_MATCH = 1;
	private string[] yy_error_string = {
		"Error: Internal error.\n",
		"Error: Unmatched input.\n"
	};
	private void yy_error(int code, bool fatal)
	{
		System.Console.Write(yy_error_string[code]);
		System.Console.Out.Flush();
		if (fatal)
		{
			throw new System.Exception("Fatal Error.\n");
		}
	}
	private static int[][] unpackFromString(int size1, int size2, string st)
	{
		int colonIndex = -1;
		string lengthString;
		int sequenceLength = 0;
		int sequenceInteger = 0;

		int commaIndex;
		string workString;

		int[][] res = new int[size1][];
		for (int i = 0; i < size1; i++) res[i] = new int[size2];
		for (int i = 0; i < size1; i++)
		{
			for (int j = 0; j < size2; j++)
			{
				if (sequenceLength != 0)
				{
					res[i][j] = sequenceInteger;
					sequenceLength--;
					continue;
				}
				commaIndex = st.IndexOf(',');
				workString = (commaIndex == -1) ? st :
					st.Substring(0, commaIndex);
				st = st.Substring(commaIndex + 1);
				colonIndex = workString.IndexOf(':');
				if (colonIndex == -1)
				{
					res[i][j] = System.Int32.Parse(workString);
					continue;
				}
				lengthString =
					workString.Substring(colonIndex + 1);
				sequenceLength = System.Int32.Parse(lengthString);
				workString = workString.Substring(0, colonIndex);
				sequenceInteger = System.Int32.Parse(workString);
				res[i][j] = sequenceInteger;
				sequenceLength--;
			}
		}
		return res;
	}
	private int[] yy_acpt = {
		/* 0 */ YY_NOT_ACCEPT,
		/* 1 */ YY_NO_ANCHOR,
		/* 2 */ YY_NO_ANCHOR,
		/* 3 */ YY_NO_ANCHOR,
		/* 4 */ YY_NO_ANCHOR,
		/* 5 */ YY_NO_ANCHOR,
		/* 6 */ YY_NO_ANCHOR,
		/* 7 */ YY_NO_ANCHOR,
		/* 8 */ YY_NO_ANCHOR,
		/* 9 */ YY_NO_ANCHOR,
		/* 10 */ YY_NO_ANCHOR,
		/* 11 */ YY_NO_ANCHOR,
		/* 12 */ YY_NO_ANCHOR,
		/* 13 */ YY_NO_ANCHOR,
		/* 14 */ YY_NO_ANCHOR,
		/* 15 */ YY_NO_ANCHOR,
		/* 16 */ YY_NO_ANCHOR,
		/* 17 */ YY_NO_ANCHOR,
		/* 18 */ YY_NOT_ACCEPT,
		/* 19 */ YY_NO_ANCHOR,
		/* 20 */ YY_NO_ANCHOR,
		/* 21 */ YY_NOT_ACCEPT,
		/* 22 */ YY_NO_ANCHOR,
		/* 23 */ YY_NOT_ACCEPT,
		/* 24 */ YY_NO_ANCHOR,
		/* 25 */ YY_NOT_ACCEPT,
		/* 26 */ YY_NO_ANCHOR,
		/* 27 */ YY_NOT_ACCEPT,
		/* 28 */ YY_NO_ANCHOR,
		/* 29 */ YY_NOT_ACCEPT,
		/* 30 */ YY_NOT_ACCEPT,
		/* 31 */ YY_NOT_ACCEPT
	};
	private int[] yy_cmap = unpackFromString(1, 130,
"21:9,22,23,21,22,23,21:18,22,21,20,21:5,5,6,4,2,21,3,21:2,19:10,21,1,21,7,2" +
"1:3,18:3,14,13,18,16,18,10,18:2,15,18,11,17,8,18,9,18,12,18:6,21:37,0:2")[0];

	private int[] yy_rmap = unpackFromString(1, 32,
"0,1:8,2,3,4,1,4,1:4,5,6,1,7,8,9,10,11,12,4,1,13,14,15")[0];

	private int[][] yy_nxt = unpackFromString(16, 24,
"1,2,3,4,5,6,7,8,9,19:4,22,19,24,26,19:2,10,11,20,12:2,-1:33,18,-1:9,28,-1:2" +
"3,10,-1:5,27:19,13,27:2,-1:11,29,-1:32,28,-1:18,14,-1:20,21,-1:7,28,-1:16,1" +
"5,-1:24,23,-1:5,28,-1:16,30,-1:28,25,-1,28,-1:15,31,-1:29,16,-1:18,17,-1:11");

	public BASICLLVM.Symbol next_token()
	{
		int yy_lookahead;
		int yy_anchor = YY_NO_ANCHOR;
		int yy_state = yy_state_dtrans[yy_lexical_state];
		int yy_next_state = YY_NO_STATE;
		int yy_last_accept_state = YY_NO_STATE;
		bool yy_initial = true;
		int yy_this_accept;

		yy_mark_start();
		yy_this_accept = yy_acpt[yy_state];
		if (YY_NOT_ACCEPT != yy_this_accept)
		{
			yy_last_accept_state = yy_state;
			yy_mark_end();
		}
		while (true)
		{
			if (yy_initial && yy_at_bol) yy_lookahead = YY_BOL;
			else yy_lookahead = yy_advance();
			yy_next_state = YY_F;
			yy_next_state = yy_nxt[yy_rmap[yy_state]][yy_cmap[yy_lookahead]];
			if (YY_EOF == yy_lookahead && true == yy_initial)
			{
				return null;
			}
			if (YY_F != yy_next_state)
			{
				yy_state = yy_next_state;
				yy_initial = false;
				yy_this_accept = yy_acpt[yy_state];
				if (YY_NOT_ACCEPT != yy_this_accept)
				{
					yy_last_accept_state = yy_state;
					yy_mark_end();
				}
			}
			else
			{
				if (YY_NO_STATE == yy_last_accept_state)
				{
					throw (new System.Exception("Lexical Error: Unmatched Input."));
				}
				else
				{
					yy_anchor = yy_acpt[yy_last_accept_state];
					if (0 != (YY_END & yy_anchor))
					{
						yy_move_end();
					}
					yy_to_mark();
					switch (yy_last_accept_state)
					{
						case 1:
							break;
						case -2:
							break;
						case 2:
							{ return new BASICLLVM.Symbol(BASICLLVM.Symbol.sym.SEMI); }
						case -3:
							break;
						case 3:
							{ return new BASICLLVM.Symbol(BASICLLVM.Symbol.sym.PLUS); }
						case -4:
							break;
						case 4:
							{ return new BASICLLVM.Symbol(BASICLLVM.Symbol.sym.MINUS); }
						case -5:
							break;
						case 5:
							{ return new BASICLLVM.Symbol(BASICLLVM.Symbol.sym.TIMES); }
						case -6:
							break;
						case 6:
							{ return new BASICLLVM.Symbol(BASICLLVM.Symbol.sym.LPAREN); }
						case -7:
							break;
						case 7:
							{ return new BASICLLVM.Symbol(BASICLLVM.Symbol.sym.RPAREN); }
						case -8:
							break;
						case 8:
							{ return new BASICLLVM.Symbol(BASICLLVM.Symbol.sym.EQUALS); }
						case -9:
							break;
						case 9:
							{ return new BASICLLVM.Symbol(BASICLLVM.Symbol.sym.INTVAR, yytext()); }
						case -10:
							break;
						case 10:
							{ return new BASICLLVM.Symbol(BASICLLVM.Symbol.sym.INTLITERAL, yytext()); }
						case -11:
							break;
						case 11:
							{ System.Console.Error.WriteLine("Illegal character: " + yytext()); break; }
						case -12:
							break;
						case 12:
							{ /* ignore white space. */break; }
						case -13:
							break;
						case 13:
							{ 
								string literal = yytext();
								return new BASICLLVM.Symbol(BASICLLVM.Symbol.sym.STRINGLITERAL, literal.Substring(1,literal.Length-2));
							}
						case -14:
							break;
						case 14:
							{ return new BASICLLVM.Symbol(BASICLLVM.Symbol.sym.END); }
						case -15:
							break;
						case 15:
							{ return new BASICLLVM.Symbol(BASICLLVM.Symbol.sym.LET); }
						case -16:
							break;
						case 16:
							{ return new BASICLLVM.Symbol(BASICLLVM.Symbol.sym.GOTO); }
						case -17:
							break;
						case 17:
							{ return new BASICLLVM.Symbol(BASICLLVM.Symbol.sym.PRINT); }
						case -18:
							break;
						case 19:
							{ return new BASICLLVM.Symbol(BASICLLVM.Symbol.sym.INTVAR, yytext()); }
						case -19:
							break;
						case 20:
							{ System.Console.Error.WriteLine("Illegal character: " + yytext()); break; }
						case -20:
							break;
						case 22:
							{ return new BASICLLVM.Symbol(BASICLLVM.Symbol.sym.INTVAR, yytext()); }
						case -21:
							break;
						case 24:
							{ return new BASICLLVM.Symbol(BASICLLVM.Symbol.sym.INTVAR, yytext()); }
						case -22:
							break;
						case 26:
							{ return new BASICLLVM.Symbol(BASICLLVM.Symbol.sym.INTVAR, yytext()); }
						case -23:
							break;
						case 28:
							{ return new BASICLLVM.Symbol(BASICLLVM.Symbol.sym.INTVAR, yytext()); }
						case -24:
							break;
						default:
							yy_error(YY_E_INTERNAL, false); break;
					}
					yy_initial = true;
					yy_state = yy_state_dtrans[yy_lexical_state];
					yy_next_state = YY_NO_STATE;
					yy_last_accept_state = YY_NO_STATE;
					yy_mark_start();
					yy_this_accept = yy_acpt[yy_state];
					if (YY_NOT_ACCEPT != yy_this_accept)
					{
						yy_last_accept_state = yy_state;
						yy_mark_end();
					}
				}
			}
		}
	}
}

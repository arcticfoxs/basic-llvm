namespace BASICLLVM.AST
{
	class Line_GoSub : Line
	{
		int gotoTarget;

		public Line_GoSub(int target)
		{
			gotoTarget = target;
		}
	}
}

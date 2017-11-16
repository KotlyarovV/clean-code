using System.Linq;

namespace Chess
{
	public class ChessProblem
	{
		private static Board board;
		public static ChessStatus ChessStatus;

		public static void LoadFrom(string[] lines)
		{
			board = new BoardParser().ParseBoard(lines);
		}

	    private static bool MoveIsCheck(Location locFrom, Location locTo)
	    {
	        var old = board.GetPiece(locTo);

	        board.Set(locTo, board.GetPiece(locFrom));
	        board.Set(locFrom, null);

	        bool hasMoves = IsCheck(PieceColor.White);

	        board.Set(locFrom, board.GetPiece(locTo));
	        board.Set(locTo, old);

	        return hasMoves;
	    }

	    private static bool HasMoves()
	    {
	        var hasMoves = false;
	        foreach (var locFrom in board.GetPieces(PieceColor.White))
	        {
	            foreach (var locTo in board.GetPiece(locFrom).GetMoves(locFrom, board))
	            {
	                if (!MoveIsCheck(locFrom, locTo))
	                    hasMoves = true;
	            }
	        }
	        return hasMoves;
	    }


	    public static ChessStatus GetChessStatus(bool isCheck, bool hasMoves)
	    {
	        if (isCheck)
	            return (hasMoves) ? ChessStatus.Check : ChessStatus.Mate;
	        return (hasMoves) ? ChessStatus.Ok : ChessStatus.Stalemate;
        }

        // Определяет мат, шах или пат белым.
        public static void CalculateChessStatus(PieceColor color)
		{
			var isCheck = IsCheck(color);
		    var hasMoves = HasMoves();
		    ChessStatus = GetChessStatus(isCheck, hasMoves);
		}
        
	    private static bool IsCheck(PieceColor color)
	    {
	        return board
                .GetPieces(color.OppositeColor())
	            .SelectMany(loc => board.GetPiece(loc).GetMoves(loc, board))
                .Any(destination => board.GetPiece(destination).Is(color, PieceType.King));   
	    }

	    
    }

    public static class PieceColorExtension
    {
        public static PieceColor OppositeColor(this PieceColor color) =>
            (color == PieceColor.Black) ? PieceColor.White : PieceColor.Black;


    }
}
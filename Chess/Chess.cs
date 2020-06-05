using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Chess
    {
        public string fen { get; private set; }
        Board board;
        Moves moves;
        List<FigureMoving> allMoves;

        public Chess(string fenStr = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            fen = fenStr;
            board = new Board(fen);
            moves = new Moves(board);
        }

        private Chess(Board board)
        {
            this.board = board;
            this.fen = board.fen;
            moves = new Moves(board);
        }

        public Chess Move(string move)
        {
            FigureMoving fm = new FigureMoving(move);
            if (!moves.CanMove(fm) || board.IsCheckAfterMove(fm))
                return this;

            Board nextBoard = board.Move(fm);
            Chess nextChess = new Chess(nextBoard);
            return nextChess;
        }

        public char GetFigureAt(int x, int y)
        {
            Square square = new Square(x, y);
            Figure fg = board.GetFigureAt(square);
            return fg == Figure.none ? '.' : (char)fg;
        }

        public void FindAllMoves()
        {
            allMoves = new List<FigureMoving>();
            foreach (FigureOnSquare fs in board.YieldFigures())
            {
                foreach (Square to in Square.YieldSquares())
                {
                    var fm = new FigureMoving(fs, to);
                    if (moves.CanMove(fm))
                        if (!board.IsCheckAfterMove(fm))
                            allMoves.Add(fm);
                }
            }
        }

        public bool isStalemate()
        {
            FindAllMoves();
            if (allMoves.Count == 0 && !board.IsCheck())
                return true;
            else
                return false;
        }

        public bool isCheckmate()
        {
            FindAllMoves();
            if (allMoves.Count == 0 && board.IsCheck())
                return true;
            else
                return false;
        }

        public string isPromotion(string move)
        {
            if (move[0] == 'P' && move[4] == '8')
                return "Q";
            if (move[0] == 'p' && move[4] == '1')
                return "q";
            return "";
        }

        public List<string> GetAllMoves()
        {
            FindAllMoves();
            var list = new List<string>();
            foreach (FigureMoving fm in allMoves)
                list.Add(fm.ToString());
            return list;
        }
    }
}

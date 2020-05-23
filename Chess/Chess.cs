﻿using System;
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

        public Chess(string fenStr = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            fen = fenStr;
            board = new Board(fen);
        }

        private Chess(Board board)
        {
            this.board = board;
            this.fen = board.fen;
        }

        public Chess Move(string move)
        {
            FigureMoving fm = new FigureMoving(move);
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
    }
}

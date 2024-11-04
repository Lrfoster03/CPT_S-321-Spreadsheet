// <copyright file="Workbook.cs" company="Logan Foster">
// 11754587
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using SpreadsheetEngine.Changes;

    /// <summary>
    /// Initializes a new instance of the <see cref="Workbook"/> class.
    /// </summary>
    internal class Workbook
    {
        private readonly Stack<ArrayList> undoStack;
        private readonly Stack<ArrayList> redoStack;

        /// <summary>
        /// Initializes a new instance of the <see cref="Workbook"/> class.
        /// </summary>
        public Workbook()
        {
            this.undoStack = new Stack<ArrayList>();
            this.redoStack = new Stack<ArrayList>();
        }

        /// <summary>
        /// Adding to our undo stack.
        /// </summary>
        /// <param name="changes">Our arraylist of changes to add to our undo/redo stacks.</param>
        public void AddUndoStack(ArrayList changes)
        {
            this.undoStack.Push(changes);
        }

        /// <summary>
        /// Adding to our undo stack.
        /// </summary>
        /// <param name="changes">Our arraylist of changes to add to our undo/redo stacks.</param>
        public void AddRedoStack(ArrayList changes)
        {
            this.redoStack.Push(changes);
        }

        /// <summary>
        /// Undo method.
        /// </summary>
        /// <returns>Returns our necessary changes.</returns>
        public ArrayList Undo()
        {
            if (this.undoStack.Any())
            {
                ArrayList changes = this.undoStack.Pop();
                return changes;
            }
            else
            {
                return new ArrayList();
            }
        }

        /// <summary>
        /// Redo method.
        /// </summary>
        /// <returns>Returns the list of changes we need.</returns>
        public ArrayList Redo()
        {
            if (this.redoStack.Any())
            {
                return this.redoStack.Pop();
            }
            else
            {
                return new ArrayList();
            }
        }

        /// <summary>
        /// Clears the RedoStack.
        /// </summary>
        public void ClearRedo()
        {
            this.redoStack.Clear();
        }

        /// <summary>
        /// Clears the UndoStack.
        /// </summary>
        public void ClearUndo()
        {
            this.undoStack.Clear();
        }

        /// <summary>
        /// Clears both our undo and redo stacks.
        /// </summary>
        public void ClearStacks()
        {
            this.ClearRedo();
            this.ClearUndo();
        }

        /// <summary>
        /// Returns if our undoStack is empty.
        /// </summary>
        /// <returns>Bool of if stack is empty.</returns>
        public bool UndoStackEmpty()
        {
            return this.undoStack.Count == 0;
        }

        /// <summary>
        /// Returns if our redoStack is empty.
        /// </summary>
        /// <returns>Bool of if stack is empty.</returns>
        public bool RedoStackEmpty()
        {
            return this.redoStack.Count == 0;
        }

        /// <summary>
        /// Returns the kind of change we would perform.
        /// </summary>
        /// <returns>String of change.</returns>
        public string KindOfChangeUndo()
        {
            if (this.undoStack.Any() && this.undoStack.Peek() != null && this.undoStack.Peek()[0]?.GetType() == typeof(ColorChanges))
            {
                return "background color change";
            }
            else
            {
                return "text change";
            }
        }

        /// <summary>
        /// Returns the kind of change we would perform.
        /// </summary>
        /// <returns>String of change.</returns>
        public string KindOfChangeRedo()
        {
            if (this.redoStack.Any() && this.redoStack.Peek()[0]?.GetType() == typeof(ColorChanges))
            {
                return "background color change";
            }
            else if (this.redoStack.Any() && this.redoStack.Peek()[0]?.GetType() == typeof(TextChanges))
            {
                return "text change";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}

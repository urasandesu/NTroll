using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NTroll.FormulaSample5.Formulas;
using Urasandesu.NAnonym.Mixins.Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Mock
{
    public class ReflectiveMethodDesigner   // Eval(() => Dsl.End()); で確定させるイメージ。
    {
        ExpressionToFormulaState state;
        public ReflectiveMethodDesigner()
        {
            state = new ExpressionToFormulaState();
        }

        public void Eval(Expression<Action> exp)
        {
            if (state.IsEnded)
            {
                throw new NotSupportedException("The internal DSL has already ended.");
            }
            exp.Body.EvalTo(state);
            if (state.IsEnded)
            {
                // ・state をチェックし、終了していたら以下の処理を開始する（ふつコンの流れを参考にすると良さげ）。
                //   ・参照の解決。
                //   ・型チェック、戻り値の確定。
                //   ・無駄な Convert の排除（最適化）。
                //   ・IL の生成。
                var noActionVisitor = new FormulaNoActionVisitor();
                var convertReducer = new ConvertReducer(noActionVisitor);
                var convertIncreaser = new ConvertIncreaser(convertReducer);
                state.CurrentBlock.Accept(convertIncreaser);
                Formula.Pin(state.CurrentBlock);
            }
        }

        public string Dump()
        {
            return state.CurrentBlock.ToString();
        }
    }

    class ConvertIncreaser : FormulaAdapter
    {
        public ConvertIncreaser(IFormulaVisitor visitor)
            : base(visitor)
        {
        }
    }
}

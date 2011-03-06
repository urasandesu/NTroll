using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Formulas;
using Urasandesu.NAnonym.Mixins.Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym;

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
                // NOTE: The visitor chain is applied order by FILO.
                var visitor = default(IFormulaVisitor);
                visitor = new FormulaNoActionVisitor();
                visitor = new ConvertDecreaser(visitor);
                visitor = new ConvertIncreaser(visitor);
                state.CurrentBlock.Accept(visitor);
                Formula.Pin(state.CurrentBlock);
            }
        }

        public string Dump()
        {
            return state.CurrentBlock.ToString();
        }
    }
}

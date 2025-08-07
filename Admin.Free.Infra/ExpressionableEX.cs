using System.Linq.Expressions;

namespace Admin.Free.Infra
{
    public class ExpressionableEX
    {
        private readonly Type _type;
        private readonly ParameterExpression _parameter;
        private List<(Func<Expression, Expression, BinaryExpression> func, Expression expr)> expressions = new();
        private ExpressionableEX(Type type)
        {
            _type = type;
            _parameter = Expression.Parameter(type);
        }

        public static ExpressionableEX Create(Type type)
        {
            return new ExpressionableEX(type);
        }
        public static ExpressionableEX Create<T>()
        {
            return new ExpressionableEX(typeof(T));
        }

        /// <summary>
        /// AND
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propname"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ExpressionableEX AndAlso<T>(string propname, T value)
        {
            var prop = Expression.Property(_parameter, propname);

            Expression<Func<T>> func = () => value;
            var convert = Expression.ConvertChecked(func.Body, prop.Type);
            var equal = Expression.Equal(prop, convert);

            var andFunc = (Expression left, Expression right) => Expression.AndAlso(left, right);
            expressions.Add((andFunc, equal));

            return this;
        }

        /// <summary>
        /// OR
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propname"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ExpressionableEX OrElse<T>(string propname, T value)
        {
            var prop = Expression.Property(_parameter, propname);

            Expression<Func<T>> func = () => value;
            var convert = Expression.ConvertChecked(func.Body, prop.Type);
            var equal = Expression.Equal(prop, convert);

            var orFunc = (Expression left, Expression right) => Expression.OrElse(left, right);
            expressions.Add((orFunc, equal));

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public LambdaExpression ToLambda()
        {
            //default
            LambdaExpression lambda = () => true;
            if (expressions.Count() > 0)
            {
                var _body = expressions[0]!.expr;
                foreach (var item in expressions[1..])
                {
                    _body = item.func(_body, item.expr);
                }
                lambda = Expression.Lambda(_body, _parameter);
            }
            return lambda;
        }
    }


    public class ExpressionableEX<T>
    {
        private readonly Type _type;
        private readonly ParameterExpression _parameter;
        private List<(Func<Expression, Expression, BinaryExpression> func, Expression expr)> expressions = new();
        private ExpressionableEX()
        {
            var type = typeof(T);
            _type = type;
            _parameter = Expression.Parameter(type);
        }

        public static ExpressionableEX<T> Create()
        {
            return new ExpressionableEX<T>();
        }

        /// <summary>
        /// AND
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propname"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ExpressionableEX<T> AndAlso(Expression<Func<T, bool>> predicate)
        {
            var br = Expression.Invoke(predicate, _parameter);
            var andFunc = (Expression left, Expression right) => Expression.AndAlso(left, right);
            expressions.Add((andFunc, br));
            return this;
        }

        /// <summary>
        /// OR
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propname"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ExpressionableEX<T> OrElse(Expression<Func<T, bool>> predicate)
        {
            var br = Expression.Invoke(predicate, _parameter);
            var andFunc = (Expression left, Expression right) => Expression.OrElse(left, right);
            expressions.Add((andFunc, br));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Expression<Func<T, bool>> ToLambda()
        {
            //default
            Expression<Func<T, bool>> lambda = (T t) => true;
            if (expressions.Count() > 0)
            {
                var _body = expressions[0]!.expr;
                foreach (var item in expressions[1..])
                {
                    _body = item.func(_body, item.expr);
                }
                lambda = Expression.Lambda<Func<T, bool>>(_body, _parameter);
            }

            return lambda;
        }

    }

}

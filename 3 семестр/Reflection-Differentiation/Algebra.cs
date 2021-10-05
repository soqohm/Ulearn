using System;
using System.Linq.Expressions;

namespace Reflection.Differentiation
{
    public static class Algebra
    {
        public static Expression<Func<double, double>> Differentiate(
            Expression<Func<double, double>> tree)
        {
            return Expression.Lambda<Func<double, double>>(
                Differentiate(tree.Body), tree.Parameters);
        }

        public static Expression Differentiate(Expression expression)
        {
            if (expression is ConstantExpression)
                return Expression.Constant(0.0);

            if (expression is ParameterExpression)
                return Expression.Constant(1.0);

            if (expression is BinaryExpression)
                return BinaryDifferentiate(expression);

            if (expression is MethodCallExpression)
                return MethodDifferentiate(expression);

            throw new ArgumentException($"{expression}  NotSupportedSyntax");
        }

        public static Expression BinaryDifferentiate(Expression expression)
        {
            var binary = (BinaryExpression)expression;

            if (expression.NodeType == ExpressionType.Add)
                return Expression.Add(
                    Differentiate(binary.Left), Differentiate(binary.Right));

            if (expression.NodeType == ExpressionType.Multiply)
                return Expression.Add(
                    Expression.Multiply(Differentiate(binary.Left), binary.Right),
                    Expression.Multiply(Differentiate(binary.Right), binary.Left));

            return binary;
        }

        public static Expression MethodDifferentiate(Expression expression)
        {
            var method = (MethodCallExpression)expression;
            var args = method.Arguments[0];

            if (method.Method.Name == "Sin")
                return Expression.Multiply(
                    Differentiate(args),
                    Expression.Call(
                        typeof(Math).GetMethod("Cos", new[] { typeof(double) }), args));

            if (method.Method.Name == "Cos")
                return Expression.Multiply(
                    Expression.Constant(-1.0),
                    Expression.Multiply(
                        Differentiate(args),
                        Expression.Call(
                            typeof(Math).GetMethod("Sin", new[] { typeof(double) }), args)));

            throw new ArgumentException($"{expression}  UnknownFunction");
        }
    }
}
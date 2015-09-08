namespace CQRS {
    public interface IQueryProcessor {
        TResult Process<TResult>(IQuery<TResult> query);
    }
}

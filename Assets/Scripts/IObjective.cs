public interface IObjective {
    bool IsCompleted(Inventory inventory);
    string Description { get; }
}
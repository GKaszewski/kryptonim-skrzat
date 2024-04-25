public interface IItem {
    string Name { get; }
    void OnPickup(Inventory inventory);
}
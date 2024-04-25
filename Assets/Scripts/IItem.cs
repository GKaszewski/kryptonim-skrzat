public interface IItem {
    string Name { get; }
    void Use(CharacterAttributes character);
    void OnPickup(Inventory inventory);
}
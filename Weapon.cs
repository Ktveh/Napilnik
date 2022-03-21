public class Weapon
{
    private int _damage;
    private int _countBullets;

    public Weapon(int damage, int countBullets)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException();
        _damage = damage;

        if (countBullets < 0)
            throw new ArgumentOutOfRangeException();
        _countBullets = countBullets;
    }

    public void Fire(Player player)
    {
        if ((!player.IsDead) && (_countBullets > 0))
        {
            _countBullets--;
            player.TakeDamage(_damage);
        }
    }
}

public class Player
{
    private int _health;

    public bool IsDead => _health <= 0;

    public Player(int health)
    {
        if (health <= 0)
            throw new ArgumentOutOfRangeException();
        _health = health;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health < 0)
            _health = 0;
    }
}

public class Bot
{
    private Weapon _weapon;

    public Bot(Weapon weapon)
    {
        _weapon = weapon;
    }

    public void OnSeePlayer(Player player)
    {
        _weapon.Fire(player);
    }
}
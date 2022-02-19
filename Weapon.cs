class Weapon
{
    private int _damage;
    private int _bullets;

    public void Fire(Player player)
    {
        if (_bullets > 0)
        {
            _bullets -= 1;
            player.TakeDamage(_damage);
        }
    }
}

class Player
{
    private int _health;

    public void TakeDamage(int damage)
    {
        if (_damage > 0)
            _health -= damage;
        if (_health < 0)
            _health = 0;
    }
}

class Bot
{
    private Weapon _weapon;

    public void OnSeePlayer(Player player)
    {
        _weapon.Fire(player);
    }
}
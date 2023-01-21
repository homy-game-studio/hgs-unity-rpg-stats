[![semantic-release](https://img.shields.io/badge/%20%20%F0%9F%93%A6%F0%9F%9A%80-semantic--release-e10079.svg)](https://github.com/semantic-release/semantic-release)
[![openupm](https://img.shields.io/npm/v/com.hgs.tone?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.hgs.rpg-stats/)

# Introduction

**HGS Rpg Stats** implements `Stats` and `Vitals` RPG elements to simplify your combat systems, idle games, shooters, and others. It's modular, scalable, event based and supports value modifiers.

See this quick sample: 

```cs
using HGS.RpgStats

public class UnitStats: MonoBehaviour
{
  public Stat damage = new Stat();
  public Stat maxHealth = new Stat();
  public Vital health = new Vital();

  void Awake()
  {
    // Seed initial values
    damage.Set(10);
    maxHealth.Set(100);

    // Add max value to health
    // Internally, health listen maxHealth changes and reply in health.onChange
    health.Max = maxHealth;

    // Refill current health
    health.Refill();
  }

  public void ApplyDamage(UnitStats enemy)
  {
    // Reduce current health from enemy damage
    health.Decrease(enemy.damage);
  }
}
```

## Modifiers
The modifiers are used to increase/decrease a stat value.

Adding modifiers

```cs
Modifier modifier = new Modifier
{
  id = "IncreaseDamage",
  operation = ModifierOperation.Sum,
  usage = ModifierUsage.Persistent,
  value = 20,
};

Stat damage = new State();
damage.Set(10);

// After, new damage value is 30 
damage.AddModifier(modifier);
```

Removing modifiers

```cs
// After, damage value is back to 10 (initial)
damage.RemoveModifier(modifier);
```

You can also remove modifier by id

```cs
// After, damage value is back to 10 (initial)
damage.RemoveModifier("IncreaseDamage");
```

About `ModifierOperation`:

| Name      | Description |
| --- | --- |
| `Sum`     | Increase a raw number, sample: (current)`100` + (value)`10` = `110`  |
| `Percentage`     | Increase current value * percentile, sample:  (current)`100` + (current)`100` * (value)`0.1f` = `110`  |

About `ModifierUsage`:

| Name      | Description |
| --- | --- |
| `Consumable`     | Is't stored, values are applyed directly`  |
| `Persistent`     | Is stored and can be removed in futhur using `stat.RemoveModifier()`  |

## Events

`Stat` events

| Name      | Description |
| --- | --- |
| `onChange(info)`      | The resources is changed  |

`Vital` events

| Name      | Description |
| --- | --- |
| `onChange(info)`      | The resources is changed  |
| `onEmpty`      | The resource is exhausted (zero)  |
| `onFull`      | The resource is reached max capacity  |

Events sample

```cs
using HGS.RpgStats

public class UnitStats: MonoBehaviour
{
  public Stat maxHealth = new Stat();
  public Vital health = new Vital();

  void Awake()
  {
    maxHealth.Set(100);
    health.Max = maxHealth;

    health.onChange+=OnHealthChange;
    health.onFull+=OnHealthFull;
    health.onEmpty+=OnHealthEmpty;

    health.Increase(9999);
    health.Decrease(10);
    health.Decrease(9999);
  }

  void OnHealthChange(ChangeInfo info)
  {
    Debug.Log("Health Changed to: "+info.current);
  }

  void OnHealthFull()
  {
    Debug.Log("Health is full!");
  }

  void OnHealthEmpty()
  {
    Debug.Log("Health is empty! Player Death?");
  }
}
```

## Installation

OpenUPM:

`openupm add com.hgs.rpg-stats`

Package Manager:

`https://github.com/homy-game-studio/hgs-unity-rpg-stats.git#upm`

Or specify version:

`https://github.com/homy-game-studio/hgs-unity-rpg-stats.git#1.0.0`

# Samples

You can see all samples directly in **Package Manager** window.

# Contrib

If you found any bugs, have any suggestions or questions, please create an issue on github. If you want to contribute code, fork the project and follow the best practices below, and make a pull request.

## Namespace Convention

To avoid script collisions, all scripts of this package is covered by `HGS.RpgStats` namespace.

## Branchs

- `master` -> Keeps the unity project to development purposes.
- `upm` -> Copy of folder content `Assets/Package` to release after pull request in `master`.

Whenever a change is detected on the `master` branch, CI gets the contents of `Assets/Package`, and pushes in `upm` branch.

## Commit Convention

This package uses [semantic-release](https://github.com/semantic-release/semantic-release) to facilitate the release and versioning system. Please use angular commit convention:

```
<type>(<scope>): <short summary>
  │       │             │
  │       │             └─⫸ Summary in present tense. Not capitalized. No period at the end.
  │       │
  │       └─⫸ Commit Scope: Namespace, script name, etc..
  │
  └─⫸ Commit Type: build|ci|docs|feat|fix|perf|refactor|test
```

`Type`.:

- build: Changes that affect the build system or external dependencies (example scopes: package system)
- ci: Changes to our CI configuration files and scripts (example scopes: Circle, - BrowserStack, SauceLabs)
- docs: Documentation only changes
- feat: A new feature
- fix: A bug fix
- perf: A code change that improves performance
- refactor: A code change that neither fixes a bug nor adds a feature
- test: Adding missing tests or correcting existing tests

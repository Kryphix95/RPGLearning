# RPGLearning

`RPGLearning` ist ein persönliches C#-Lernprojekt, in dem ich schrittweise die technische Grundlage für ein eigenes rundenbasiertes RPG entwickle.

Der aktuelle Schwerpunkt liegt bewusst auf dem **Combat-Core, Klassenidentitäten und einer modularen Architektur**. Die Konsolenoberfläche dient momentan ausschließlich als Testumgebung für die Spiellogik und ist **nicht als finale Benutzeroberfläche gedacht**. Sobald die grundlegenden Combat-Systeme stabil genug sind, soll das Projekt in Richtung eines ersten **Unity-Prototyps** weitergeführt werden.

## Aktueller Stand

Der Combat-Prototyp besitzt inzwischen eine zusammenhängende Grundlage aus mehreren miteinander arbeitenden Systemen:

- Character- und Enemy-Grundsystem
- Many-vs-Many-Kämpfe mit dynamischer Turn Order
- physische und magische Angriffe
- zentrale `DamageResult`- und `DamageTaken()`-Pipeline
- Crit-, Dodge-, Block- und Damage-Reduction-Logik
- Damage Types und elementare Resistances
- Physical, Fire, Ice, Lightning, Holy und Dark als Damage Types
- Immunitäten, Resistenzen und Schwächen über die zentrale Damage-Pipeline
- Mana-basiertes Ability-System
- funktionierendes Cooldown-System
- Status-Effect-Lifecycle mit Apply-, Turn- und Remove-Logik
- Stun, Slow und Haste
- Damage-over-Time mit Poison und Bleed
- Heal-over-Time / Regeneration
- Refresh identischer Status-Effekte statt mehrfaches Stacken
- klassenbezogene Ability-Kits für Warrior, Rogue, Cleric und Mage

## Aktuelle Basisklassen

### Warrior

Der Warrior konzentriert sich auf direkten physischen Schaden, Kontrolle und Defensive.

- `Heavy Strike` – verstärkter physischer Angriff
- `Shield Bash` – reduzierter Schaden mit Stun
- `Iron Guard` – temporär 30 % weniger eingehender Physical Damage
- `Hamstring` – reduzierter Schaden mit Slow

### Rogue

Der Rogue nutzt schnelle physische Angriffe und Damage-over-Time-Mechaniken.

- `Double Slash`
- `Poison Strike`
- `Rending Slash`
- `Rupture`

### Cleric

Der Cleric verbindet Healing, Support und Holy Damage.

- `Small Regeneration`
- `Minor Heal`
- `Haste`
- `Minor Smite`

### Mage

Der Mage bildet die erste Grundlage für elementare Magie und unterschiedliche Element-Identitäten.

- `Small Fireball`
- `Ice Shard` – Ice Damage mit Slow
- `Shock` – Lightning Damage mit Stun-Chance
- `Meditation` – stellt Mana wieder her

Die aktuellen Werte und Skill-Kits dienen vor allem dazu, die zugrunde liegenden Systeme praktisch zu entwickeln und zu testen. Sie sind noch kein finales Balancing.

## Architektur & Lernziel

Das Projekt entsteht bewusst schrittweise. Neue Fähigkeiten sollen möglichst auf bestehenden Systemen aufbauen, statt ihre eigene Sonderlogik zu duplizieren.

Ein wichtiges Ziel ist deshalb, zentrale Regeln wie Damage-Mitigation, Resistances, Status Effects und Combat-Ergebnisse an möglichst wenigen Stellen zu bündeln. Dadurch sollen spätere Änderungen und Balancing-Anpassungen nicht das manuelle Überarbeiten zahlreicher einzelner Abilities erfordern.

Gleichzeitig dient das Projekt dazu, C# nicht nur über isolierte Übungen zu lernen, sondern anhand eines wachsenden Systems mit echten Abhängigkeiten, Refactorings und Designentscheidungen.

## Aktueller Fokus

Die grundlegenden Ability-Kits der vier derzeitigen Basisklassen stehen. Als nächste Entwicklungsschritte sind unter anderem vorgesehen:

- Ability-Targeting und `TargetCount`
- sauberere Trennung von Spiellogik und Console-/Input-Ausgabe
- weitere Combat-Edge-Cases und Systemtests
- Weiterentwicklung der Damage- und Stat-Skalierung
- Ausbau von Elementen, Resistances und Status-Interaktionen
- Vorbereitung der bestehenden Combat-Logik für einen späteren Unity-Prototyp

## Langfristige Richtung

Langfristig soll das Projekt unter anderem folgende Systeme enthalten:

- Party- und Roster-System
- Klassen, Spezialisierungen und unterschiedliche Endklassen
- Equipment mit Waffen- und Klassenrestriktionen
- umfangreichere Ability- und Status-Systeme
- unterschiedliche Damage Types und Element-Identitäten
- Loot, Inventar und Progression
- Enemy Knowledge / Bestiary
- optionale Bosse und Superbosse
- eine spätere 2D-Spielwelt mit Encountern und weiteren RPG-Systemen

Die detaillierteren Designideen, bereits getroffenen Entscheidungen und offenen Fragen werden separat im [`Game Design Document`](docs/GAME_DESIGN.md) festgehalten.

## Technischer Stand

- **Sprache:** C#
- **Framework:** .NET 10
- **Aktuelles Interface:** Console als Testoberfläche
- **Geplante spätere Engine:** Unity
- **Projektstatus:** aktive Entwicklung / Lernprojekt

Viele Zahlen, Formeln und einzelne Mechaniken sind derzeit bewusst noch provisorisch. Der Fokus liegt momentan stärker auf einer funktionierenden und erweiterbaren Grundlage als auf finalem Balancing.

## Starten

Mit einem passenden .NET SDK kann das Projekt im Repository-Verzeichnis über

```bash
dotnet run --project Game.csproj
```

gestartet werden.

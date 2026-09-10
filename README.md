# RPGLearning

`RPGLearning` ist ein persönliches C#-Lernprojekt, mit dem ich schrittweise ein modulares, rundenbasiertes RPG-System entwickle.

Der aktuelle Schwerpunkt liegt bewusst auf dem **Combat-Core und der dahinterliegenden Architektur**. Die derzeitige Konsolenoberfläche dient nur als Testumgebung für die Spiellogik und ist **nicht als finale Benutzeroberfläche gedacht**. Sobald der erste Combat-Core stabil genug ist, soll das Projekt in Richtung **Unity-Prototyp** weitergeführt werden.

## Aktueller Stand

Bereits umgesetzt sind unter anderem:

- Character- und Enemy-Grundsystem
- Kämpfe mit mehreren Teilnehmern
- dynamische Zugreihenfolge über Dexterity
- physische Angriffe mit Crit, Dodge, Block und Damage Reduction
- `DamageResult` als zentrale Rückgabe für Kampfergebnisse
- Ability-System mit Mana-Kosten
- Status-Effect-Lifecycle mit `OnApply`, `OnTurnStart`, `OnTurnEnd` und `OnRemove`
- Stun / Action Prevention
- Damage-over-Time-Grundsystem
- Poison inklusive Laufzeit und Damage pro Turn
- Refresh bereits vorhandener identischer Status-Effekte statt mehrfaches Stacken

## Aktueller Fokus

Als Nächstes stehen vor allem folgende Punkte an:

- funktionierendes Cooldown-System
- weitere kleine Abilities und Status Effects zur Wiederholung und Festigung von C#
- Ability-Targeting und TargetCount
- Grundlage für Damage Types, Elemente und Resistenzen
- wichtige Combat-Edge-Cases testen
- Spiellogik stärker von Console-/Input-Ausgabe trennen

Danach ist ein erster kleiner **Unity-Combat-Prototyp** geplant.

## Langfristige Richtung

Das Projekt soll langfristig unter anderem folgende Systeme enthalten:

- Party- und Roster-System
- Klassen, Spezialisierungen und unterschiedliche Endklassen
- Equipment mit Waffen- und Klassenrestriktionen
- Fähigkeiten, Status Effects und verschiedene Damage Types
- Loot, Inventar und Progression
- Enemy Knowledge / Bestiary
- optionale Bosse und Superbosse
- spätere 2D-Spielwelt mit Encountern und weiteren RPG-Systemen

Die detaillierteren Designideen und bereits getroffenen Entscheidungen werden separat in [`docs/GAME_DESIGN.md`](docs/GAME_DESIGN.md) festgehalten.

## Technischer Stand

- Sprache: **C#**
- aktuelles Interface: **Console** als Testoberfläche
- geplante spätere Engine: **Unity**

Das Projekt befindet sich aktiv in Entwicklung. Viele Werte, Formeln und einzelne Systeme sind derzeit bewusst noch provisorisch und werden im Laufe des Lern- und Entwicklungsprozesses weiter überarbeitet.

## Starten

Mit einem passenden .NET SDK kann das Projekt im Repository-Verzeichnis beispielsweise über

```bash
dotnet run --project Game.csproj
```

gestartet werden.

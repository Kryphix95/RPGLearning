# RPGLearning — Game Design Document

**Version:** 0.1  
**Stand:** 2026-09-10  
**Status:** Living Design Document  
**Zweck:** Dieses Dokument hält die aktuell beschlossenen Designregeln, starken Richtungen, offenen Fragen und bewusst geparkten Ideen fest. Es soll verhindern, dass spätere Implementierungsentscheidungen die bereits gewählte Spielidentität verwässern oder widersprechen.

---

## 0. Status-Legende

- **FIXED** — bewusste Designentscheidung; spätere Systeme sollen sich daran orientieren.
- **STRONG DIRECTION** — sehr wahrscheinliche Richtung; Details dürfen sich noch ändern.
- **OPEN** — bewusst noch nicht entschieden.
- **BACKLOG** — gute Idee, aber aktuell kein Implementierungsziel.
- **EXAMPLE** — Beispiel zur Illustration, nicht automatisch verbindlicher Content.

---

# 1. Core Vision & Design Philosophy

## 1.1 Klassenidentität vor Universalität — FIXED

Das Spiel soll vermeiden, dass jeder Charakter am Ende alles kann. Klassen und Spezialisierungen sollen eine echte Identität besitzen.

Grundsätze:

- **Class identity over universality.**
- **Meaningful choices over cosmetic choices.**
- **System interaction over isolated mechanics.**
- **Depth through combinations, not unnecessary complexity.**
- Spezialisierungen sollen Möglichkeiten eröffnen **und** andere Möglichkeiten schließen.
- Charaktere sollen durch Klasse, Waffen, Fähigkeiten, Ausrüstung, Rollen und Synergien klar voneinander unterscheidbar sein.
- Eine frühere Klassenstufe soll nicht automatisch nur ein wertloses Wartezimmer für die nächste sein.

## 1.2 Oldschool + modern — FIXED

Das Spiel soll bewusst Elemente älterer RPGs mit moderner technischer Struktur kombinieren.

Gewünscht sind insbesondere:

- Konsequenzen statt vollständiger Reversibilität.
- Discovery statt permanenter Tutorials.
- Missable Content.
- versteckte Bedingungen.
- hohe, aber nachvollziehbare Härte.
- Respekt vor Spielerbeobachtung und Experimentierfreude.
- moderne Build-Tiefe und technische Wartbarkeit.

Nicht jede Information muss im Voraus erklärt werden. Ein Spieler darf scheitern, beobachten, lernen und später mit mehr Wissen zurückkommen.

## 1.3 Entscheidungen müssen Gewicht haben — FIXED

Spielerentscheidungen dürfen langfristige oder endgültige Folgen haben.

Das betrifft unter anderem:

- Klassenwahl.
- Spezialisierungen.
- Waffen-/Equipment-Zugriff.
- Ability-Zugriff.
- Party-/Roster-Entwicklung.
- optionale Quests.
- versteckte Weltzustände.
- spätere Boss-Kompatibilität.
- Endgame-Belohnungen.

Nicht jede Entscheidung muss frei rückgängig gemacht werden können.

## 1.4 Ein Playthrough muss nicht alles zeigen — FIXED

Der Spieler wird **nicht** darauf ausgelegt, in einem Durchlauf automatisch jede Fähigkeit, Quest, Waffe, Spezialisierung oder jeden versteckten Inhalt zu entdecken.

Ein zweiter Run darf durch Spielerwissen fundamental anders verlaufen.

## 1.5 Theoretische 100%-Möglichkeit vs. Garantie — FIXED

Es darf theoretisch einen Entwicklungsweg geben, mit dem der komplette Content in einem einzigen Playthrough spielbar ist.

Das Spiel garantiert dies jedoch nicht.

Irreversible Entscheidungen bei:

- Klassen,
- Spezialisierungen,
- Partyentwicklung,
- Roster-Balance,
- Equipment,
- versteckten Flags,

können dazu führen, dass einzelne optionale Inhalte auf einem konkreten Save praktisch oder mathematisch nicht mehr lösbar sind.

---

# 2. Difficulty & Fairness Philosophy

## 2.1 Story muss grundsätzlich machbar bleiben — FIXED

Jeder **legal entwickelte** Spielstand muss die Hauptstory grundsätzlich abschließen können.

Das bedeutet ausdrücklich **nicht**, dass jeder Build gleich angenehm durch die Story kommt.

Eine schlecht zum Boss passende Party darf:

- erheblich mehr Vorbereitung verlangen,
- andere aktive Charaktere verlangen,
- Gear-Anpassungen verlangen,
- Ability-Anpassungen verlangen,
- deutlich mehr Versuche kosten,
- im Extremfall stundenlange Progression erzeugen.

Die Story darf den Spieler für schlechte Synergien bluten lassen, aber keinen permanenten Softlock erzeugen, der allein aus einer früheren legalen Entscheidung entsteht.

## 2.2 Optionaler Endgame-Content darf Builds ausschließen — FIXED

Superbosse und andere optionale Endgame-Begegnungen müssen nicht mit jedem finalisierten Setup lösbar sein.

Ein Boss darf beispielsweise sein:

- massiver Physical-DPS-Gegencheck,
- Element-Check,
- Sustain-Check,
- Burst-Window-Check,
- Anti-Crit-Encounter,
- Status-/Cleanse-Check,
- Party-Composition-Check,
- Mechanik-vor-Damage-Encounter.

Wenn ein finalisiertes Roster genau die falschen Wege gewählt hat, darf die Antwort lauten:

> Dieser Boss ist mit diesem Save-Zustand nicht mehr realistisch oder mathematisch lösbar.

Das ist bei optionalem Content eine bewusste Designfolge und keine automatische Designpanne.

## 2.3 Schwierigkeit durch Wissen — FIXED

Bosswissen ist Teil der Spielerprogression.

Der Spieler muss nicht beim ersten Kontakt verstehen, warum etwas passiert.

Gewünscht ist ein Lernprozess wie:

1. Scheitern.
2. Muster erkennen.
3. Hypothese bilden.
4. testen.
5. Regel verstehen.
6. Lösung anwenden.

Mechaniken sollen konsistent genug sein, um gelernt werden zu können, aber nicht zwingend sofort transparent sein.

## 2.4 Kontrollierte „Asshole“-Mechaniken — FIXED

Das Spiel darf absichtlich fies sein.

Beispiele:

- seltene zufällige Single-Target-One-Shots im späten Endgame,
- versteckte Trigger,
- ungewöhnliche Resistenzwechsel,
- verzögerte Effekte,
- überraschende Regelbrüche.

Grenze:

- reine Zufallsmechaniken sollen nicht ständig ohne Gegenmöglichkeit die komplette Party löschen.
- Auf Party-Ebene soll eine Chance auf Recovery bestehen.

---

# 3. Combat Core — Grundregeln

## 3.1 Kampfstruktur — FIXED

- Turn-based JRPG-orientiertes Kampfsystem.
- Many-vs-Many.
- gewünschte aktive Spielerparty: maximal **4**.
- gewünschte Gegneranzahl: bis zu **6**.
- Limits sollen eher Encounter-/Party-Regeln sein als hart im Turnloop verankerte Speziallogik.
- genau **eine Aktion pro Charakter pro Runde**.
- Aktionen werden sofort beim jeweiligen Charakter ausgeführt; keine globale Action Queue.
- Tote Charaktere werden übersprungen.
- Kampf endet sofort, sobald eine Seite keine lebenden Mitglieder mehr besitzt.

## 3.2 Turn Order — FIXED

- Reihenfolge richtet sich dynamisch nach aktueller **Dexterity/Speed**.
- Haste/Slow/Dexterity-Änderungen können die noch verbleibende Reihenfolge derselben Runde beeinflussen.
- Bereits handelnde Charaktere handeln nicht erneut in derselben Runde.
- Ein Start-of-Round-Snapshot bestimmt, wer grundsätzlich in dieser Runde einen Turn bekommen kann.
- Neu gespawnte Teilnehmer können sofort existieren und Ziel sein, erhalten ihren ersten eigenen Turn aber erst in der nächsten Runde.

## 3.3 Player Targeting — FIXED

Normale Spielerangriffe dürfen grundsätzlich jedes lebende Character-Ziel treffen:

- Gegner,
- Verbündete,
- den eigenen Charakter selbst.

Das ist absichtliche Oldschool-Flexibilität.

Enemy-Normal-Attacks wählen nur lebende Spielercharaktere.

Abilities erhalten später eigene Target-Regeln und TargetCount.

## 3.4 Status Timing — FIXED

- Statusdauer wird am Ende des **eigenen Turns des betroffenen Charakters** reduziert.
- DoT-Effekte ticken am Ende des betroffenen Turns.
- Stun verhindert die eigentliche Aktion, aber der Charakter erhält weiterhin seinen Turn.
- Start-/End-of-Turn-Statuslogik läuft auch bei Stun.
- Stored Status Effects müssen mindestens 1 Turn Dauer besitzen.
- reine One-Shot-Effekte mit 0 Turn Dauer müssen nicht als persistenter Status gespeichert werden.

## 3.5 Status Reapplication — FIXED

Exakt derselbe konkrete Status-Typ stackt nicht mehrfach.

Bei erneutem Anwenden:

- Dauer wird auf `max(existing, new)` aktualisiert.
- Dauer wird niemals verkürzt.
- `OnApply` wird nicht erneut ausgelöst.

Unterschiedliche konkrete Status-Typen können parallel existieren.

Beispiel:

- WeakPoison + HeavyPoison: möglich.
- Poison + Poison desselben Typs: Refresh statt Stack.

## 3.6 Status Application — FIXED

Status wird nur angewandt, wenn der relevante Hit bzw. die relevante Bedingung erfolgreich war.

Ein Dodge verhindert OnHit-/OnCrit-artige Folgeeffekte.

Block zählt weiterhin als erfolgreicher Hit.

## 3.7 Crit / Dodge / Block — FIXED

- Crit + Hit = Crit.
- Crit + Block = Crit + Block.
- Crit-Roll + Dodge = kein gültiger Crit; `IsCrit` wird nicht als erfolgreicher Crit gewertet.
- Crit soll langfristig grundsätzlich für alle Schadensarten möglich sein.
- Dodge soll grundsätzlich möglich sein, sofern eine Ability es nicht explizit überschreibt.
- Blockierbarkeit einzelner magischer/elementarer Angriffe bleibt designabhängig.

## 3.8 Cooldowns — STRONG DIRECTION / NEXT IMPLEMENTATION

Ein funktionales Cooldown-System ist der nächste größere Combat-Schritt.

Voraussichtliche Struktur:

- Ability besitzt Basis-Cooldown.
- Ability besitzt RemainingCooldown.
- Cooldown zählt auf den eigenen Turns des Besitzers herunter.
- genaue Off-by-One-Semantik muss vor Implementierung festgelegt werden.
- Mana, Cooldown und Target-Validität sollen später vor Ausführung geprüft werden.
- ungültige Aktionen sollen langfristig **keinen Turn verbrauchen**.

---

# 4. Damage, Elements & Resistances

## 4.1 MagicalDamage ist Skalierungswert, kein finaler Damage Type — FIXED

`MagicalDamage` dient als Hintergrund-Offensivwert, nicht als sichtbare finale Schadenskategorie.

Magische Abilities sollen konkrete Elemente verursachen, z. B.:

- Fire,
- Ice,
- Lightning,
- Holy,
- Dark.

Weitere Elemente bleiben offen.

## 4.2 Element-Identitäten — FIXED CONCEPTUAL DIRECTION

Elemente sollen sich mechanisch unterscheiden und nicht nur farbige Kopien sein.

Beispiele:

- **Fire:** hoher Schaden, Burn/DoT, weniger Kontrolle, eventuell längere Cooldowns.
- **Ice:** geringere Multiplikatoren, Slow/Freeze/Control.
- **Lightning:** mittlerer Schaden, Stun/Tempo/Multi-Hit-Möglichkeiten.
- **Holy:** niedrigere reine Damage-Leistung, Healing/Support, eigene Resistance.
- **Dark:** zwischen Fire/Lightning, Drain/Debuffs/Sonderinteraktionen.

Die exakten Zahlen sind offen.

## 4.3 Zentraler Damage Pipeline — FIXED TECHNICAL DIRECTION

Defense-/Resistance-/Mitigation-Formeln sollen zentralisiert sein.

Späteres Ziel:

- DamageResult kennt genug Kontext über konkreten Schaden/Element.
- Resistances werden zentral angewandt.
- Balancing-Änderungen sollen nicht das manuelle Editieren dutzender Abilities erfordern.

Aktuelle DoTs dürfen vorübergehend direkt HP reduzieren; langfristig soll die Damage Pipeline diese Interaktionen sauber abbilden können.

---

# 5. Party & Roster

## 5.1 Main Character ist normalerweise Pflichtmitglied — FIXED

Der Spielercharakter muss grundsätzlich Teil der aktiven Party sein.

Ausnahmen sind explizite Story-/Gameplay-Sequenzen.

Dadurch hat die Main-Class-Wahl direkte Auswirkungen auf die spätere Party-Komposition.

## 5.2 Party-NPCs werden vom Spieler kontrolliert — FIXED

Storyrolle und Combat-Kontrolle sind getrennt.

Companions besitzen eigene Persönlichkeit und Vergangenheit, werden im Kampf aber vom Spieler gesteuert.

## 5.3 Unterschiedliche Progressionsfreiheit bei Companions — FIXED

Es existieren mehrere Companion-Typen:

### A) Starter-/Core-Charaktere

- besitzen den vollständigen Entwicklungsweg.
- Spieler entscheidet ihre Spezialisierungen.
- Spieler kann gegebenenfalls bewusst auf einer früheren Klassenstufe bleiben.
- höchste Build-Freiheit.

### B) wenige ausgewählte Story-Charaktere

- besitzen teilweise oder vollständig festgelegte Entwicklungswege.
- frühere Entscheidungen sind Teil ihrer Geschichte.
- Spieler kann diese Vergangenheit nicht rückgängig machen.
- weitere Feinabstimmung kann trotzdem offen sein.

### C) spätere reguläre Recruits

- steigen auf einer bereits fortgeschrittenen/mittleren Klassenstufe ein.
- ihre Vergangenheit bis zu diesem Punkt ist fest.
- verbleibende Endpfade bzw. spätere Spezialisierungen kann der Spieler wählen.

Prinzip:

> Companion past = fixed; companion future = partly player-shaped.

## 5.4 Roster statt nur „beste Viererparty“ — FIXED

Der Spieler soll langfristig nicht nur vier Lieblingscharaktere optimieren, sondern ein funktionierendes **Roster** entwickeln können.

Strategische Fragen sollen sein:

- Habe ich genug Healing?
- Habe ich Tankiness?
- Habe ich Physical Damage?
- Habe ich Elemental/Magic Damage?
- Habe ich Burst?
- Habe ich Sustain?
- Habe ich Utility?
- Habe ich unterschiedliche Resistenz-/Status-Antworten?

Ein Charakter darf 80 % der Zeit auf der Bank sitzen und für einen bestimmten Boss plötzlich essenziell werden.

## 5.5 Main-Class kann Companion-Klasse beeinflussen — OPEN

Mögliche Idee:

Bestimmte Companions könnten abhängig von der gewählten Main-Class mit anderer Klasse oder anderem Entwicklungsweg auftreten, obwohl Story und Dialoge weitgehend gleich bleiben.

Ziel wäre Roster-Varianz und geringere Doppelung.

Nicht für alle NPCs geeignet; einige Klassen sollen Teil ihrer festen Identität bleiben.

---

# 6. Class Progression

## 6.1 Limited Base Classes — FIXED DIRECTION

Der Main Character soll nur aus einem begrenzten Satz von Starter-/Base Classes wählen können.

Nicht jede im Spiel existierende Klasse muss für den Main verfügbar sein.

Companion-exclusive Special Classes sind ausdrücklich erwünscht.

Exakte Anzahl und Namen der Base Classes: **OPEN**.

## 6.2 Reverse Engineering der Klassen — STRONG DIRECTION

Klassen sollen bevorzugt von ihrer gewünschten Endgame-Identität rückwärts konstruiert werden.

Reihenfolge:

1. Wie soll sich die Endklasse spielen?
2. Welche Waffen verkörpern das?
3. Welche Rüstung/Stats passen dazu?
4. Welche Fähigkeiten braucht sie?
5. Was muss sie dafür aufgeben?
6. Welche gemeinsame Vorstufe ergibt daraus Sinn?

Dies soll verhindern, dass Zwischenklassen nur künstliche Füllknoten werden.

## 6.3 Mehrstufige Spezialisierung — STRONG DIRECTION

Grundidee:

- Base Class.
- erste große Entwicklung/Mittelklasse.
- spätere Endklasse/zweite Spezialisierung.

Möglicherweise zwei echte Spezialisierungspunkte pro vollständig frei entwickelbarem Charakter.

Grobe mögliche Levelmarken:

- erster großer Pfad ungefähr Level 35.
- zweiter ungefähr Level 70.

Exakte Level: **OPEN**.

## 6.4 Spezialisierung muss echte Wahl sein — FIXED

Eine Spezialisierung darf nicht einfach „+10 % besser“ bedeuten.

Sie darf:

- neue Waffen öffnen,
- alte Waffen schließen,
- Ability-Zugriff verändern,
- Rüstungskategorien verändern,
- Ressourcen verändern,
- Core-Loops verändern,
- Equipment-Pools verändern,
- neue Synergien erzeugen,
- bestehende Flexibilität reduzieren.

## 6.5 Auf früherer Stufe bleiben kann Endgame-Pfad sein — FIXED

Nicht weiter zu spezialisieren darf eine bewusste Endgame-Entscheidung sein.

Beispiel:

- Rogue bleibt Rogue.
- Assassin/Ninja werden nicht gewählt.
- Rogue erhält dafür eigene Mastery-/Endgame-Waffen oder andere exklusive Vorteile.

Prinzip:

> Spezialisierung reduziert Breite und erhöht Tiefe.

Der unspezialisierte/zwischenstufige Pfad darf seine eigene Endgame-Identität besitzen.

## 6.6 Endklasse ist schärfste Klassenidentität — FIXED

Je weiter die Spezialisierung fortschreitet, desto kleiner darf der verfügbare Werkzeugkasten werden.

Dafür sollen die verbleibenden Werkzeuge stärker aufeinander abgestimmt sein.

---

# 7. Ability Progression & Mastery

## 7.1 Combat kennt Ability, nicht ihre Unlock-Quelle — FIXED TECHNICAL DIRECTION

Der Combat-Core soll langfristig nur wissen:

> Dieser Character besitzt diese Ability.

Woher die Ability kam, gehört nicht in den Combat-Core.

## 7.2 Ability Unlock Sources — STRONG DIRECTION

Abilities können über verschiedene Quellen gelernt werden:

- Level-Meilensteine.
- Quests.
- Trainer/Shop.
- Skill Scrolls/Tomes.
- Boss Drops.
- Rare Mob Drops.
- Exploration.
- Spezialisierung.
- Mastery.
- versteckte Bedingungen.

Nicht jede Ability muss jedem Charakter offenstehen.

## 7.3 Mastery / Ability Constellation — STRONG DIRECTION

Gewünschte visuelle Richtung:

- 2D-Node-Netzwerk / Constellation / Mastery Grid.
- zentraler Startpunkt.
- verbundene Cluster.
- isolierte oder versteckte Nodes.
- frei verschiebbare 2D-Ansicht.

Mögliche Informationsstufen:

1. vollständig sichtbar/known.
2. sichtbar als `?`.
3. vollständig verborgen bis Trigger.

Nodes können geben:

- Abilities.
- Passives.
- Ability-Upgrades.
- neue Branches.
- Specialization Gates.

## 7.4 Talent Tree vs. Ability Network — OPEN

Mögliche Trennung:

- Ability Network bestimmt, **welche Skills** gelernt werden.
- Talent Tree verbessert bestehende Skills/Mechaniken.

Mögliche Talentpunkt-Frequenz: etwa alle 5 Level.

Noch nicht fest.

## 7.5 Skill-by-use Mastery — OPEN

„Benutze Skill, um Mastery zu erhöhen“ wurde erwogen.

Problem:

- triviale Gegner könnten zum Spam-Exploit werden.

Nur sinnvoll, wenn relevante Gegner-/Situationsbedingungen existieren.

---

# 8. Weapon & Equipment Philosophy

## 8.1 Weapon Progression follows Class Progression — FIXED

Base Classes besitzen kleine, überschaubare Waffenpools.

Mit Spezialisierung:

- entstehen neue Waffenpools,
- werden vorhandene Pools enger,
- werden Waffen charakteristischer,
- werden klassenspezifische Interaktionen stärker.

Frühere kompatible Waffen dürfen teilweise weiter nutzbar bleiben, müssen aber nicht optimal sein.

## 8.2 Spezialisierung darf Waffen entfernen — FIXED

Spezialisierungen dürfen frühere Waffenfamilien komplett verlieren.

Beispiele:

- Cleric-Endklasse: kein Staff mehr.
- Assassin: nur noch Dagger-artige Waffen; frühere Bow-Abilities können dadurch nicht mehr nutzbar sein.

## 8.3 Abilities dürfen Equipment-Voraussetzungen besitzen — FIXED

Abilities können an mechanische Voraussetzungen gebunden sein, z. B.:

- Weapon Type.
- Shield equipped.
- Class.
- Specialization.
- Status/Resource.

Dadurch kann ein Waffenverlust automatisch auch den Zugriff auf bestimmte frühere Skills entfernen.

## 8.4 Gemeinsame Waffenfamilie ≠ gleiche Spielweise — FIXED

Mehrere Klassen dürfen dieselbe Waffenfamilie benutzen.

Beispiel:

- Battle Mage und Chanter benutzen beide Combat Staff.
- ihre bevorzugten Substats, Effekte und Abilities unterscheiden sich trotzdem stark.

Das Spiel soll nicht künstlich für jede Klasse eine eigene Weapon Type erfinden müssen.

## 8.5 Shared Equipment has no intended-class label — FIXED

Wenn mehrere Klassen ein Item mechanisch tragen dürfen, sagt das Item **nicht**, für wen es „gedacht“ ist.

Keine Anzeigen wie:

- „Best for Cleric“.
- Sterne-Rating pro Klasse.
- empfohlene Klasse.

Das Item zeigt:

- Item Type.
- Stats.
- Effekte.
- echte harte Requirements.

Der Spieler entscheidet anhand des Builds, wer es sinnvoll nutzt.

Prinzip:

> Class restriction is a mechanical rule, not an item recommendation.

## 8.6 Endgame Equipment Tiers — FIXED DIRECTION

Mögliche Abstufung:

1. allgemeine Weapon Type.
2. Progression-/Specialization-gebundene Weapon Type.
3. gemeinsame Endgame-Waffe für verwandte Endklassen.
4. extrem spezialisierte class-exclusive Signature-/Legendary-Waffe.

Beispiel:

- Dagger: mehrere passende Klassen.
- Master Dagger: nur nach zweiter Spezialisierung, Assassin + Ninja.
- Assassin Relic Dagger: Assassin only.
- Ninja Relic Dagger: Ninja only.

---

# 9. Loot Philosophy

## 9.1 No Smart Loot — FIXED

Loot passt sich nicht automatisch an die aktuelle Party oder Klasse des Spielers an.

Boss und Welt besitzen ihren Loot unabhängig vom Build.

Das bedeutet:

- ein Ninja kann einen Assassin-Dagger finden.
- ein physisches Roster kann eine Mage-Waffe erhalten.
- ein Save kann eine mächtige Waffe bekommen, die niemand aktuell nutzen kann.

Das ist erlaubt und gewollt.

## 9.2 Optionaler Loot darf „gemein“ sein — FIXED

Insbesondere bei:

- Superbossen,
- Rare Drops,
- Hidden Quests,
- Endgame-Belohnungen,

muss nicht garantiert sein, dass die Belohnung für den aktuellen Build optimal oder überhaupt nutzbar ist.

## 9.3 Signature / Legendary Weapons — STRONG DIRECTION

Jede finale Endklasse soll langfristig mindestens eine echte Signature-/Legendary-Waffe besitzen.

Diese soll:

- Klassenmechaniken verstärken.
- neue Synergien erzeugen.
- nicht nur „mehr Attack“ sein.
- die Endklassenfantasie sichtbar machen.

Beispiele:

- Assassin Legendary verstärkt Crit/Poison/Extra-Turn-Interaktion.
- Cleric Legendary verstärkt Block/Healing/Sustain.
- Chanter Legendary verstärkt Staff/Buff/Hybrid-Loop.
- Berserker Legendary verstärkt Risk/Reward und Lifesteal.

Konkrete Effekte: **OPEN**.

## 9.4 Anzahl der Legendaries pro Run — OPEN / STRONG DIRECTION

Wahrscheinliche Richtung:

- nicht jede Legendary muss in einem Run erhältlich sein.
- grob mehrere starke Endgame-Waffen, z. B. etwa vier, könnten ein sinnvoller Zielwert sein.
- dadurch trägt eine aktive Viererparty möglicherweise nur 1–2 gleichzeitig, abhängig vom Encounter.

Noch nicht fest.

## 9.5 Sehr niedrige Drop Rates sind erlaubt — FIXED PHILOSOPHY

Sehr seltene Drops dürfen existieren:

- 0,5 %.
- 0,1 %.
- eventuell 0,01 % bei wirklich besonderem optionalem Content.

Grind ist akzeptabel, wenn die Belohnung einzigartig und bedeutungsvoll ist.

Nicht gewünscht:

- tausende austauschbare Stat-Sticks ohne mechanische Identität.

## 9.6 Sockets / Manastones — OPEN

Aion-artige Sockel-/Manastone-Systeme sind interessant, aber noch nicht fest.

Sie dürfen nur hinzukommen, wenn sie echte Build-Tiefe erzeugen und nicht unnötig aufblasen.

---

# 10. Enemy Knowledge / Bestiary System

## 10.1 Knowledge System existiert — FIXED

Es soll ein Bestiary-/Enemy-Knowledge-System geben, das Wissen schrittweise aus tatsächlicher Spielerfahrung aufbaut.

Der Eintrag ist nicht automatisch vollständig, nur weil ein Gegner einmal gesehen wurde.

## 10.2 Knowledge soll Beobachtung abbilden — FIXED

Mögliche Informationen:

- Name.
- Level.
- HP.
- Effective / Weaknesses.
- Resistant.
- Immune.
- bekannte Abilities.
- Ability-Beschreibungen.
- eventuell weitere Special Traits.

Informationen können durch Handlungen freigeschaltet werden.

Beispiele:

- Physical Attack macht 0 Damage → Physical Immunity kann entdeckt werden.
- Ability wird benutzt → Ability-Name wird bekannt.
- Effekt wird erlebt → qualitative Beschreibung wird ergänzt.

## 10.3 Repeatable Enemies — FIXED DIRECTION

Wiederholbare Gegner dürfen über längere Zeit teilweise unbekannt bleiben.

Ein Gegner muss nicht nach einem Kill automatisch zu 100 % bekannt sein.

Exakte Unlock-Regeln: **OPEN**.

## 10.4 Unique / One-Time Enemies — FIXED

Einmalige Gegner und dauerhaft missbare Bosse folgen während des Kampfes denselben Discovery-Regeln.

Nach dem endgültigen Sieg wird ihr verfügbarer Bestiary-Eintrag jedoch auf **100 % Knowledge** vervollständigt.

Grund:

- sonst könnten Informationen nach dem letzten möglichen Encounter dauerhaft unerreichbar bleiben.
- der vollständige Eintrag dient als zusätzliche Belohnung und Bestätigung nach dem Sieg.

## 10.5 Ability Descriptions im Bestiary — FIXED

Eine vollständige Beschreibung soll spielrelevante Informationen ausreichend erklären, ohne zwangsläufig interne Formeln offenzulegen.

Wichtige zeitliche Regeln sollen genannt werden.

Beispiele:

- Duration: 5 Turns.
- Effekt tritt am Ende des Turns auf.
- Trickster-Effekt hält 10 Turns.

Bei komplexen Superboss-Abilities darf nach dem Sieg auch logische Bedingung erklärt werden, z. B.:

- Ability 3 wird nur eingesetzt, wenn Condition A/B nicht erfüllt wurde.
- eine Fähigkeit invertiert bestimmten Schaden.

Nicht notwendig:

- exakter Sourcecode.
- jede interne Random-Formel.
- Debug-Werte.

## 10.6 Information Philosophy — FIXED

Eigene Spielerfähigkeiten und bekannte Effekte müssen nicht zwingend mathematisch vollständig beschrieben werden.

Beispiele:

Möglich:

> Haste — erhöht Speed für 5 Turns.

statt zwingend:

> +30 % Dexterity.

Möglich:

> Poison Strike — Chance, Poison anzuwenden.

statt zwingend:

> exakt 50 %, 2 % Max HP usw.

Allerdings:

- **Dauer** zeitabhängiger Effekte soll grundsätzlich sichtbar sein, wenn sie bekannt/beschrieben sind.
- Beschreibungen müssen innerhalb des Spiels konsistent sein.

Der Spieler darf viele exakte Werte durch Beobachtung, Vergleiche und Erfahrung lernen.

Prinzip:

> Das Spiel darf Informationen liefern, ohne sie immer als Formeltext auszuschreiben.

## 10.7 Gegnermechaniken müssen nicht vorher erklärt werden — FIXED

Bossmechaniken, Resistances, versteckte Phase-Trigger und Encounter-Lösungen müssen nicht im Voraus offenliegen.

Der Spieler darf sie erst im Kampf lernen.

---

# 11. Boss & Encounter Design

## 11.1 Bosses are not required to disclose mechanics — FIXED

Bossfähigkeiten müssen vor oder während des ersten Versuchs nicht vollständig erklärt werden.

Der Spieler darf lernen durch:

- sichtbare Zahlen.
- Reaktionen auf Schaden.
- veränderte Turn Order.
- Reflect.
- 0 Damage.
- Buff Icons.
- Statuswechsel.
- Bossdialoge.
- wiederholte Versuche.

## 11.2 Known Archetype + Bastard Twist — FIXED DIRECTION

Superbosse sollen häufig aus einem bekannten Archetypen bestehen, dem 1–2 ungewöhnliche Mechaniken hinzugefügt werden, die nicht zum normalen Archetypen passen.

Beispiel:

- Templar-artiger Boss mit bekannter hoher Defense/Block.
- zusätzlich Magic Reflect oder verzögerter Death/Invert-Effekt.

Dadurch kann vorhandenes Spielerwissen helfen, ohne den Encounter vollständig zu lösen.

## 11.3 Trickster / Inversion Mechanic — STRONG DIRECTION

Eine Kefka-inspirierte Trickster-Mechanik ist ausdrücklich gewünscht.

Mögliche Form:

- bestimmter Zustand für z. B. 10 Turns.
- einzelne Damage-/Healing-Regeln werden invertiert oder verändert.
- Boss kündigt Ability an.
- Effekt tritt möglicherweise verzögert ein.
- Zwischenzustand kann mit `???` oder uneindeutigen Informationen arbeiten.

Mögliche Inversionen:

- Physical Damage → Healing.
- Fire ↔ Ice.
- Heal → Damage.
- Magical Damage wird invertiert/verändert.

Exakte Regeln: **OPEN**.

Wichtig:

- wenn eine Inversion einmal definiert ist, soll sie intern konsistent sein.

## 11.4 World-State kann Boss verändern — FIXED

Frühere Aktionen in der Welt dürfen Bossverhalten massiv verändern.

EXAMPLE:

Castle Templar Boss:

- niedriger Guard Killcount → Sword/Shield, hohe Defense, geringerer Damage.
- hoher Guard Killcount → Boss wird aggressiv, wechselt auf Greatsword, hoher Damage, sehr hohe Physical Mitigation.

Die UI muss diese versteckte Bedingung nicht erklären.

## 11.5 Frühere Phasen dürfen spätere Phasen beeinflussen — FIXED

Bosskämpfe dürfen Flags aus früheren Phasen speichern.

Eine gelöste spätere Mechanik muss nicht automatisch einen Fehler aus einer früheren Phase neutralisieren.

## 11.6 DPS Checks sind erlaubt — FIXED

Ein Boss darf explizit bestimmte Damage-Profile verlangen oder andere stark bestrafen.

Beispiele:

- Crit Immunity.
- 85 % Physical Reduction.
- hohe Poison Resistance.
- Lightning Weakness.

Ein vollständig physisches Roster kann dadurch bei optionalem Content scheitern.

---

# 12. Class Concepts

> Hinweis: Die folgenden Klassen sind teilweise bereits klar definiert, teilweise Arbeitsnamen. Baumtopologie und Namen dürfen sich noch ändern.

## 12.1 Healer / Holy Tree

### Cleric — FIXED CONCEPT / NAME TEMPORARY

Arbeitsname, da Aion-Nähe später eventuell umbenannt wird.

Identität:

- defensive Healer-Endklasse.
- One-Hand Weapon + Shield.
- **kein Staff**.
- hohe Stabilität.
- Schutz/Sustain.
- klassische Heilung.
- einige gemeinsame Skills mit Priest.
- einige exklusive defensive Tools.

### Chanter — FIXED CONCEPT / NAME TEMPORARY

Arbeitsname, wahrscheinlich später umbenennen.

Identität:

- offensiver Support-/Holy-Hybrid.
- weniger Healing als Cleric.
- mehr physischer/offensiver Beitrag.
- Buffs/Utility.
- nur **Combat Staff**.
- Waffenfamilie kann sich mit Battle Mage überschneiden.

### Priest — STRONG DIRECTION

Identität:

- stärkerer klassischer Caster-/Healer-Pfad.
- einige Überschneidungen mit Cleric.
- eigene exklusive Tools.
- Weapon/Focus eventuell Buch, Relic, Orb oder ähnlicher Artstyle.
- eventuell keine klassische Kampfwaffe, aber wahrscheinlich eigenes Equipment-Äquivalent, um Itemization nicht zu verlieren.

### offensiver Holy/Dark Caster-Pfad — STRONG DIRECTION

Neben Priest soll ein deutlich offensiverer Holy-/Dark-orientierter Pfad existieren.

Exakte Endklasse und Name: **OPEN**.

## 12.2 Martial / Heavy Tree

### Berserker — STRONG DIRECTION

Identität:

- Plate Armor.
- klassenspezifische Plate-Effekte können bewusst mehr eingehenden Schaden verursachen.
- starke Selbstheilung/Lifesteal über eigenen verursachten Schaden.
- hoher Risk/Reward.
- schwere Axt-/Heavy-Weapon-Identität.
- zusätzliche Ausnahme: Dual-Wield mit zwei Einhandwaffen möglich.

### Warrior-/Vanguard-artiger Off-Tank — STRONG DIRECTION

Identität:

- stabiler als Berserker.
- klassischer Off-Tank.
- schwere Waffen.
- weniger selbstzerstörerisch.
- konstante Frontline.

Name und genaue Baumposition: **OPEN**.

### Paladin — STRONG DIRECTION

Identität:

- klassische Tank-Endklasse.
- physischer + Holy-basierter Schaden.
- sehr hohe Stabilität.
- defensive Tools.

### Templar — STRONG DIRECTION / NAME TEMPORARY

Identität:

- beefy, offensiver als Paladin.
- Greatsword-orientiert.
- Holy-DPS-Variante.
- hohe Standfestigkeit, aber nicht derselbe Defensive-Loop wie Paladin.

### Dark Knight — STRONG DIRECTION

Identität:

- physischer + Dark Damage.
- beefy, aber andere Defensive als Paladin/Templar.
- HP als Ressource für Buffs, Schaden oder Defensive möglich.
- bewusste Interaktion mit gefährlichen HP-Schwellen.

### vierter Knight-/Heavy-Endpfad — OPEN

Noch keine überzeugende Identität festgelegt.

## 12.3 Rogue / Trickery / Martial Tree

### Assassin — FIXED DIRECTION

Identität:

- maximaler Single-Target-Burst.
- Crit.
- Poison/Bleed.
- Multi-Hit.
- Execution-artige Effekte.
- Extra-Turn-/Tempo-Synergien möglich.
- stark spezialisierte Dagger-Waffen.

### Ninja — STRONG DIRECTION

Identität:

- mehr Debuffs.
- mehr Gruppen-Utility.
- weniger reiner Single-Target-Burst als Assassin.
- Status-/Tempo-/Control-orientierter.
- Dagger-Überschneidungen mit Assassin möglich.

### Monk Branch — STRONG DIRECTION / DETAILS OPEN

Mögliche Entwicklung aus Rogue-/Martial-Vorstufe.

Zwei mögliche Endrichtungen:

- offensiver Battle Monk / Kung-Fu-Master-artiger Pfad.
- defensiver/spiritueller Pfad.

Konkretes Core-Gimmick noch **OPEN**.

## 12.4 Fencer — STRONG DIRECTION AS SPECIAL CLASS

Fencer könnte besser als Special Class funktionieren statt in einen Hauptbaum gezwungen zu werden.

Identität:

- leichte Rüstung.
- Rapier/Einhandwaffe.
- defensive Präzision.
- Parry → Riposte / Counter-Loop.

## 12.5 Parry Mechanic — OPEN / POSSIBLY JUSTIFIED

Neue Core-Mechaniken sollen nur eingeführt werden, wenn bestehende Systeme die Klassenfantasie nicht ausreichend ausdrücken.

Fencer könnte ein sinnvoller Ausnahmefall sein.

Mögliche Mechanik:

- erfolgreicher Parry negiert oder reduziert starken Schaden.
- Parry erzeugt Counter-/Riposte-Zustand.
- nach erfolgreichem Parry werden besondere Abilities möglich.

Nicht jetzt implementieren.

## 12.6 Battle Mage — STRONG DIRECTION

Identität:

- Hybrid aus physischem und elementarem Schaden.
- einzelne Abilities können Physical + Element-Komponente verbinden.
- keine reine Caster-Kopie.
- eher Chain-artige Rüstung als aktuelle Richtung.
- Combat Staff als mögliche Waffenfamilie.
- Waffenüberschneidungen mit Chanter möglich, aber andere Substats/Abilities.

## 12.7 Hunter — STRONG DIRECTION

Hunter als Archetyp ist gewünscht.

Möglicher späterer Pfad:

- Beastmaster.
- Tamer.

Details offen.

---

# 13. Class / Equipment Interaction Rules

## 13.1 Shared Skills between Endclasses — FIXED

Endklassen dürfen Teile ihres gemeinsamen Ursprungs behalten.

Beispiel:

- Priest und Cleric teilen einige Heals.
- Cleric besitzt zusätzliche defensive Tools.
- Priest besitzt zusätzliche reine Healer-/Caster-Tools.

Keine Endklasse muss vollständig einzigartige Ability-Listen besitzen.

## 13.2 AllowedClasses und Equipment Requirements — STRONG TECHNICAL DIRECTION

Spätere Datenmodelle könnten Abilities und Equipment mit Regeln wie folgenden beschreiben:

- AllowedClasses.
- RequiredSpecialization.
- RequiredWeaponType.
- ShieldRequired.
- RequiredStatus.
- ResourceRequirement.

Das dient als Mechanik, nicht als Empfehlungssystem.

---

# 14. Information Philosophy

## 14.1 Eigene Tools dürfen qualitativ beschrieben sein — FIXED

Das Spiel muss nicht jeden Zahlenwert im Tooltip offenlegen.

Beispiele:

Akzeptabel:

> Haste — erhöht Speed für 5 Turns.

statt zwingend:

> +30 % Dexterity für exakt 5 Turns.

Akzeptabel:

> Has a chance to Poison the target.

statt zwingend:

> 50 % Chance, 2 % Max HP pro Tick.

Beide Informationsstile können fair sein, solange sie konsistent sind.

## 14.2 Zeitliche Dauer soll sichtbar sein — FIXED

Wenn ein Effekt eine definierte Turn-Dauer besitzt und seine Beschreibung freigeschaltet/bekannt ist, soll diese Dauer grundsätzlich genannt werden.

Beispiele:

- Poison: 5 Turns.
- Haste: 5 Turns.
- Trickster Inversion: 10 Turns.

Dies ist nötig, damit der Spieler taktisch planen kann.

## 14.3 Erklär nicht automatisch die Lösung — FIXED

Das Spiel darf dem Spieler sein Werkzeug erklären, ohne ihm zu sagen, wie der nächste Boss damit gelöst wird.

Beobachtbare Reaktionen sind selbst Information.

Beispiele:

- 0 Damage.
- Heal statt Damage.
- veränderte Reihenfolge.
- Reflect.
- plötzlich niedrigerer Schaden.

---

# 15. Hidden Content, Flags & Missables

## 15.1 Frühere kleine Entscheidungen dürfen sehr spät wirken — FIXED

Unscheinbare Handlungen dürfen versteckte World-State-Flags setzen.

Diese können viel später:

- Quests öffnen.
- Quests schließen.
- Items ermöglichen.
- Legendary-Komponenten ermöglichen.
- Bosse verändern.
- NPC-Verhalten verändern.

Das Spiel muss den Spieler nicht darauf hinweisen, dass die Handlung relevant war.

## 15.2 Missable Long-Term Conditions — FIXED

Optionale Endgame-Inhalte dürfen dauerhaft verpasst werden.

Beispiel:

- NPC früh genug angesprochen.
- Kapitel abgeschlossen.
- spätere Quest wird dadurch freigeschaltet.
- ohne das frühe Flag existiert die Quest auf diesem Save nicht.

## 15.3 Zeit vs. Progress Trigger — STRONG DIRECTION

Kapitel-/Event-/Region-Trigger sind meist sauberer als echte Spielzeit.

Mögliche Bedingungen:

- vor Ende Kapitel 1.
- vor Betreten Region X.
- vor Boss Y.
- vor Event Z.

Echte Spielzeit darf als seltene absichtliche Ausnahme existieren, müsste dann aber klar definieren, ob Pause/Menüs zählen.

---

# 16. UI / UX

## 16.1 Console ist nur temporäre Testoberfläche — FIXED

Die aktuelle Console ist keine geplante finale Präsentation.

Der Combat-Core soll später ausreichend von Console/Input/Thread.Sleep getrennt werden, bevor oder während der Unity-Integration.

## 16.2 Unity ist nächster großer visueller Schritt — FIXED ROADMAP

Nach Combat-v1 soll ein kleiner Unity-Prototyp folgen.

Nicht vorher noch unendlich viele neue Combat-Fundamentalsysteme hinzufügen.

## 16.3 Battle HUD — OPEN

Zu entscheiden:

- Unterschied zwischen normalen Mobs und Boss-HUD.
- Statusanzeige.
- Turn Order Darstellung.
- Zielinformationen.
- Buff/Debuff Visualisierung.

## 16.4 Target Detail Window — STRONG DIRECTION

Mögliche Lösung:

- normales Combat-HUD bleibt relativ schlank.
- Spieler kann ein eigenes Detailfenster für das aktuelle Target öffnen.
- dieses Fenster kann direkt mit dem Knowledge-System verbunden sein.
- unbekannte Informationen erscheinen als `???`.

Noch keine finale UI-Entscheidung.

---

# 17. Technical Architecture Principles

## 17.1 Core Systems sollen voneinander getrennt bleiben — FIXED

Beispiele:

- Combat führt Regeln aus.
- Enemy/Game Data beschreibt, was existiert.
- Knowledge-System speichert, was der Spieler kennt.
- UI entscheidet, was davon angezeigt wird.

Das Knowledge-System soll nicht den Combat-Core verändern müssen, nur um unbekannte Informationen zu verstecken.

## 17.2 Neue Core-Mechanik nur bei echtem Mehrwert — FIXED

Neue fundamentale Systeme sollen nur entstehen, wenn ein gewünschter Gameplay-Loop mit bestehenden Mechaniken nicht sinnvoll ausdrückbar ist.

Beispiel:

- Fencer-Parry könnte gerechtfertigt sein.
- für jede neue Klasse automatisch ein neues Subsystem zu bauen ist nicht gewünscht.

## 17.3 Zentralisierte Balance-Formeln — FIXED

Werte wie:

- Defense,
- Resistance,
- Crit,
- Dodge,
- Block,
- Damage Scaling,
- XP-Kurve,

sollen möglichst zentral oder datengetrieben definiert werden.

Ziel:

Balancing-Änderungen sollen keine großflächigen Codeänderungen verlangen.

---

# 18. World / Story / Exploration

## 18.1 Story — OPEN

Es fehlt noch eine grobe Kernidee für:

1. Wer ist der Spieler am Anfang?
2. Was bringt ihn auf die Reise?
3. Was ist der zentrale Konflikt der Welt?
4. Warum eskaliert die Reise plausibel bis zu extremen Endgame-Gegnern und Superbossen?

Die Story soll die bereits festgelegten Systemideen unterstützen, nicht gegen sie arbeiten.

## 18.2 Origin / Start Structure — OPEN

Diskutierte Möglichkeiten:

### A) verschiedene kurze Startorte

- unterschiedliche Dörfer/Quartiere.
- frühe individuelle Abschnitte.
- später schnelle Konvergenz auf gemeinsame Hauptstory/Stadt.

### B) gleicher Startort, unterschiedliche Backgrounds

- andere Familie/NPC-Beziehungen/Dialoge.
- eventuell kleine Stat-Unterschiede.

## 18.3 Background getrennt von Combat Class — STRONG DIRECTION

Background und spätere Base Class könnten getrennt sein.

Mögliche Form:

- zunächst classless/shared intro.
- später Base-Class-Wahl durch Training/Event.

Background darf auch suboptimale Stats geben.

Beispiel:

- arme Herkunft, aber viel gelesen → +Intelligence.
- später Rogue → Intelligence ist nicht automatisch optimal.

Keine Belohnung allein dafür, am Startscreen die „richtige“ Antwort zu klicken.

## 18.4 Discovery in der Welt — FIXED PHILOSOPHY

Nicht alle:

- Quests,
- NPC-Zustände,
- Items,
- Skills,
- Bosse,
- Unlocks,

müssen sichtbar oder angekündigt sein.

Der Spieler darf 30-mal mit demselben NPC sprechen, weil sich später etwas geändert haben könnte.

---

# 19. Vertical Slice / Scope

## 19.1 Scope Boundary — FIXED DIRECTION

Langfristiges Spiel:

- 2D.
- turn-based.
- single-player.

3D und Multiplayer gelten aktuell als unnötige Scope-Explosion.

## 19.2 erster sinnvoller Vertical Slice — STRONG DIRECTION

Möglicher Ablauf:

1. kleine Town.
2. Outdoor Map.
3. Bewegung.
4. Encounter.
5. Combat Scene.
6. Party-Funktionalität.
7. Skills/Status/Turn Order.
8. Victory/Defeat.
9. Rückkehr zur Map.

Danach schrittweise:

10. XP/Level.
11. Loot.
12. Inventory.
13. Shop.
14. Chest.
15. einfache NPC/Dialog-Struktur.

Story, große Questketten, Cutscenes, Savegames, Art und Audio sind noch nicht ausgearbeitet.

---

# 20. Combat-v1 Boundary Before Unity

## 20.1 Muss vor Unity abgeschlossen sein — FIXED ROADMAP

- aktuelle Bugs bereinigen.
- Cooldowns.
- ausreichend Wiederholungscontent für C#-Festigung.
- Ability Targeting + TargetCount.
- Damage Pipeline / Elements / Resistances Foundation.
- wichtige Combat Edge Cases testen.
- Console/Input/Presentation ausreichend vom Core trennen.

## 20.2 Danach: STOP für neue Combat-Fundamentalsysteme — FIXED ROADMAP

Nach Erreichen dieser Grenze geht das Projekt in einen Unity-Prototyp.

Neue große Combat-Grundsysteme werden vorher nicht mehr begonnen, außer:

- echter Bug.
- Unity-Integration wird blockiert.

Neue Ideen gehen ins Backlog.

---

# 21. Post-Unity / Later Systems — BACKLOG

Nach einem funktionierenden Unity-Combat-Proof-of-Concept:

- XP-/Level-Loop.
- Map/Encounter-System.
- Loot.
- Inventory.
- Equipment UI.
- Shop.
- Chest.
- Queststruktur.
- Bestiary/Knowledge UI.
- Legendary-System.
- Hidden World-State Flags.
- Story Content.
- vollständige Klassenbäume.
- Save/Load.

---

# 22. Current Open Questions

Diese Punkte sind bewusst **nicht** final entschieden:

- genaue Anzahl der Base Classes.
- endgültige Namen vieler Klassen.
- genaue Klassenbaum-Topologie.
- welche Zwischenklasse zu welcher Endklasse führt.
- genaue Spezialisierungslevel.
- Respec-Regeln.
- Talent Tree vs. Ability Constellation.
- Skill-by-use-Mastery.
- genaue Elemente.
- genaue Blockregeln für magischen Schaden.
- Priest-Weapon/Focus.
- vierter Knight-/Heavy-Endpfad.
- Monk-Gimmick.
- Hunter/Beastmaster/Tamer-Details.
- Fencer als Special Class vs. anderer Pfad.
- Parry-Mechanik im Detail.
- Anzahl Legendary Weapons pro Run.
- genaue Legendary-Quellen.
- Socket-/Manastone-System.
- Bestiary-Unlock-Formeln für normale Gegner.
- Boss-HUD vs. Mob-HUD.
- Target Detail Window Layout.
- Start-/Origin-Modell.
- Story/Setting.
- Queststruktur.
- Economy/Crafting.
- konkrete Zahlen und Balancing.

---

# 23. Design Guardrails

Bei jeder neuen Idee künftig prüfen:

1. **Stärkt sie Klassenidentität oder macht sie alle ähnlicher?**
2. **Erzeugt sie eine echte Entscheidung oder nur mehr Auswahl ohne Konsequenz?**
3. **Kann sie mit bestehenden Systemen ausgedrückt werden?**
4. **Falls sie eine neue Core-Mechanik braucht: erzeugt diese einen wirklich neuen Gameplay-Loop?**
5. **Passt sie zur Oldschool-Informationsphilosophie?**
6. **Bleibt Story-Content grundsätzlich lösbar?**
7. **Darf optionaler Content absichtlich Builds ausschließen?**
8. **Ist die Regel konsistent genug, dass Spieler sie lernen können?**
9. **Ist sie Combat-v1-relevant oder gehört sie ins Backlog?**
10. **Muss diese Entscheidung jetzt wirklich getroffen werden?**

---

# 24. Kurzfassung der Spielidentität

RPGLearning soll ein 2D, turn-based Singleplayer-RPG mit starkem Klassen- und Partyfokus werden.

Die zentrale Identität entsteht aus:

- eingeschränkten, gewichtigen Klassenpfaden.
- echten Spezialisierungskosten.
- starkem Roster-Building.
- klaren Weapon-/Equipment-Identitäten.
- verstecktem Wissen und Discovery.
- einem progressiven Bestiary/Knowledge-System.
- Bossen, die beobachtet und gelernt werden müssen.
- optionalem Endgame, das nicht jedem Build garantiert offensteht.
- versteckten World-State-Folgen.
- missbarem Content.
- seltenen und bedeutungsvollen Endgame-Belohnungen.
- bewusstem Verzicht auf permanente Handholding.

Das Spiel soll dem Spieler viele Werkzeuge geben, aber nicht garantieren, dass er sie sinnvoll kombiniert.

**Leitsatz:**

> Entscheidungen haben Gewicht. Wissen ist Progression. Nicht alles muss in einem Run möglich bleiben.


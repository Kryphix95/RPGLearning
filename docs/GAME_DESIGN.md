# PROJECT ASCEND — GAME DESIGN DOCUMENT

**Version:** 0.3 · **Stand:** 23.09.2026 · **Status:** Living Design Document  
**Projektname:** Ascend (Arbeitsname); technisches Repository: `RPGLearning`  
**Genre / Ziel:** Klassisches, rundenbasiertes 2D-Singleplayer-JRPG mit starker Klassen-, Build-, Party- und Encounter-Identität.  
**Dokumentzweck:** Vollständige, versionierte **Master-GDD** zur Ablage im GitHub-Repository. Sie hält beschlossene Spielregeln, bereits besprochene Detailkonzepte, aktuelle technische Realität, bewusst archivierte frühere Entwürfe und offene Fragen in getrennten Statuskategorien fest. Das Dokument ist weder ein Versprechen auf vollständige Umsetzung noch eine Liste sofort zu implementierender Features.

> **Spielidentität:** Ein klassisches, rundenbasiertes RPG, dessen Tiefe daraus entsteht, Charaktere zu spezialisierten Werkzeugen zu entwickeln und diese Werkzeuge passend zu Encountern zu kombinieren. Einfache, lesbare Grundregeln; wachsende Tiefe durch ihre Wechselwirkungen.

## 0. Leseschlüssel und Quellenstand

- **FIXED:** bewusst beschlossene Designregel; Änderung nur durch neue bewusste Entscheidung.
- **DIRECTION:** gewollte Richtung, Ausprägung und Details können sich ändern.
- **OPEN:** noch nicht entschieden; nicht durch plausibel klingende Annahmen ergänzen.
- **BACKLOG:** später denkbar, aber explizit kein Auftrag für Combat-v1.
- **IMPLEMENTED:** im C#-Prototyp vorhanden, nicht automatisch finale Game-Design-Regel.
- **TO VERIFY:** laut Entwicklungsverlauf eingebaut bzw. geplant, aber noch nicht ausreichend funktional getestet.
- **EXAMPLE:** veranschaulicht eine Regel und ist weder fertiger Content noch ein verbindlicher Zahlenwert.

**Quellenabgleich:** Ausführliche Original-GDD `docs/GAME_DESIGN.md` v0.1 vom 10.09.2026, konsolidierte Zwischenfassung v0.2 vom 23.09.2026, spätere ausdrücklich besprochene und revidierte Entscheidungen sowie verifizierter C#-Stand auf `main` vom 16.09.2026. Am 17.09. begann lokal der rein kosmetische Cleanup; gemeldete lokale Änderungen sind noch nicht als neuer Remote-Stand verifiziert. Insbesondere **geplant ≠ implementiert** und **kompiliert ≠ vollständig getestet**.

**Zentrale Änderungen gegenüber v0.1:** Cooldown, TargetType/TargetCount, Resistance/DamageType, Damage-Hook, dynamische Speed-Reihenfolge und Status-Timing sind inzwischen implementiert bzw. in Combat-v1 eingefroren. Das bisher offene „vier Base Classes“ ist als Zielmodell **4 Starter → 8 Zwischenklassen → 16 Endklassen** konkretisiert; alte Baumvorschläge sind daher nicht mehr automatisch gültige Pfade. Die derzeit 16 vorgesehenen Endjob-Namen sind erfasst, **ihre exakten Zweige aber nicht festgelegt**. Die direkte DoT-HP-Änderung ist für den derzeitigen Entwurf absichtlich, kein zu beseitigender Zwischenfehler. HoT/Heilung soll die **nominale Stärke** zeigen, nicht nur effektiv fehlende HP. Die Console ist Testadapter und wird vor Unity vom Core getrennt.

---

# 1. Vision und Designpfeiler

## 1.1 Klassenidentität vor Universalität — FIXED

Nicht jeder Charakter soll am Ende alles können. Ein Job ist kein austauschbares Zahlenpaket, sondern ein eigener Satz an Werkzeugen, Grenzen und Synergien. Spezialisierung eröffnet gezielt Möglichkeiten **und schließt andere**. Zwischenstufen sollen nicht bloß wertlose Wartezimmer sein. Gemeinsame Skills oder Waffen sind erlaubt, solange Core-Loop, Ausrüstung, Prioritäten und Teamrolle erkennbar verschieden bleiben.

**Designprinzipien:** Klassenidentität vor Universalität; bedeutsame statt kosmetische Entscheidungen; Systeminteraktionen vor isolierten Gimmicks; Tiefe durch Kombination statt unnötige Regelkomplexität. Starke bis bewusst „kaputte“ PvE-Synergien dürfen Spielerwissen belohnen; unbeabsichtigte Endlosschleifen und echte Programmfehler gehören nicht dazu.

## 1.2 Oldschool + modern — FIXED

Die Welt darf gefährlich, versteckt, missbar und in Teilen irreversibel sein. Das Spiel respektiert Experimentierfreude und Beobachtung statt permanenter Handhaltung. Gleichzeitig sollen technische Architektur, Bedienbarkeit und Systemkonsistenz modern sein. Der Spieler bekommt Regeln und Werkzeuge, aber nicht automatisch die Lösung jedes Encounters.

## 1.3 Spielerwissen ist Progression — FIXED

Gegner, Gebiete und Bosse müssen nicht beim ersten Kontakt vollständig erklärbar sein. Scheitern → Muster erkennen → Hypothese bilden → erneut versuchen → passende Klasse, Ability oder Ausrüstung einsetzen ist gewollter Fortschritt. Regeln müssen intern konsistent und durch Beobachtung lernbar bleiben.

## 1.4 Entscheidungen und Replayability — FIXED

Eine Wahl darf Auswirkungen auf Klassenwege, Waffen-/Ability-Zugriff, Party und Roster, Quests, World-State und optionale Boss-Kompatibilität haben. Ein Run muss weder alle Fähigkeiten noch alle Gegenstände und versteckten Begegnungen zeigen. Ein theoretisch möglicher 100%-Run ist nicht ausgeschlossen, aber **nicht garantiert**.


## 1.5 Nicht jeder Run ist eine Checkliste — FIXED

Ein einzelner Durchlauf soll nicht automatisch alle Systeme, Nebenquests, seltenen Waffen, Spezialisierungen und Bosse erschließen. Es darf **theoretisch** einen Weg zu sehr hoher oder vollständiger Content-Abdeckung geben, doch das Spiel verspricht weder die Auffindbarkeit noch die praktische Lösbarkeit aller optionalen Begegnungen mit jedem legalen Save. Das verhindert nicht, dass der Hauptstory-Abschluss für jeden legal entwickelten Spielstand erreichbar bleiben muss.

Die Entscheidung für einen Job, ein seltenes Item oder eine Questlinie ist keine rein kosmetische Variante; sie darf andere Zugänge dauerhaft ausschließen. Ein neuer Durchlauf mit Wissen aus dem vorherigen Run darf deshalb grundlegend andere Möglichkeiten öffnen.

## 1.6 Was die Spieler lernen sollen — FIXED PHILOSOPHY

Der Spieler soll die **Regeln des eigenen Werkzeugs** kennen können, aber nicht schon beim Betreten eines Gebiets die dazugehörige optimale Lösung. Fortschritt entsteht aus Beobachtung, langfristiger Teamplanung, eigener Hypothesenbildung und der Kombination einfacher Regeln. Ein sichtbarer Verlust oder Fehlschlag darf einen Hinweis geben; das Spiel muss daraus kein permanentes Tutorial machen.

Starke Kombinationen, einschließlich sehr leistungsfähiger PvE-Builds, sind ausdrücklich ein möglicher Lohn für Systemverständnis. Davon zu unterscheiden sind unbeabsichtigte unendliche Action-/Ressourcen-Schleifen, technische Exploits und widersprüchliche Regeln; diese gelten nicht automatisch als erwünschtes Balancing.

---

# 2. Schwierigkeit, Fairness und Welt

## 2.1 Story-Abschluss vs. optionaler Content — FIXED

Jeder **legal entwickelte Spielstand** soll die Hauptstory grundsätzlich abschließen können, ohne durch eine frühere zulässige Klassen-/Roster-Entscheidung dauerhaft gesoftlockt zu werden. Das bedeutet nicht, dass jede Party jeden Story-Boss angenehm besiegt: Vorbereitung, neues Gear, andere aktive Mitglieder und viele Versuche dürfen notwendig sein.

**Optionale Superbosse** dürfen einzelne finalisierte Builds oder sogar ganze Save-Zustände ausschließen. Anti-Crit-, Element-, Sustain-, Burst-, Status-, Recovery- und Party-Kompositionschecks sind erlaubt. Der Spieler soll nicht alles mit derselben Lieblingsviererparty lösen können müssen.

## 2.2 Härte und Zufall — FIXED

Seltene harte Einzelziel-Treffer, versteckte Trigger, Resistenzwechsel, verzögerte Effekte und Überraschungen sind zulässig. Zufällige, häufige komplette Party-Wipes ohne sinnvolle Gegenmöglichkeit sind nicht Ziel. Auf Party-Ebene soll es im Grundsatz Möglichkeiten zur Reaktion oder Recovery geben.

## 2.3 Welt-Schwierigkeit — DIRECTION

Gefährliche Regionen dürfen früh zugänglich sein; die Welt muss sich nicht automatisch ans Level des Spielers anpassen. Erkundung, Rückzug und späteres Zurückkehren sind sinnvolle Entscheidungen. Die Hauptgeschichte beginnt eher geführt und öffnet sich später; konkrete Karte, Regionenzahl und Progressionsgates sind OPEN.


## 2.4 Konkrete Grenzen der Fairness — FIXED / DIRECTION

Die Schwierigkeit darf von fehlendem Wissen, unpassender Ausrüstung und bewussten früheren Entscheidungen geprägt sein. Die Welt muss **nicht** auf das aktuelle Level skalieren; ein gefährliches Gebiet kann früh betreten und später mit anderen Werkzeugen wieder besucht werden. Der Spieler soll an Gegnerreaktionen lernen, wann Rückzug oder ein anderer Ansatz sinnvoll sind.

Für die **Hauptstory** besteht eine grundsätzliche Abschließbarkeits-Garantie für jeden legal entwickelten Save; sie ist keine Garantie auf kurze Kämpfe, niedrigen Farmaufwand oder eine bestimmte optimale Party. Für **optionale Superbosse** existiert diese Garantie ausdrücklich nicht. Seltene überraschende Single-Target-One-Shots oder verdeckte Bedingungen sind als Richtung zugelassen; wiederholte zufällige Total-Wipes ohne sinnvolle Party-Recovery sind nicht das Ziel. Konkrete Encounter-Werte bleiben offen.

---

# 3. Combat: Kernregeln und Ablauf

## 3.1 Format — FIXED / größtenteils IMPLEMENTED

- Rundenbasiert, keine ATB-Leiste, keine globale Warteschlange vorab gewählter Aktionen.
- Many-vs-Many, eine **reguläre Aktion je berechtigtem Charakter pro Runde**; Sonder-Extra-Turns erst später als ausdrücklich separate Mechanik.
- Geplante aktive Spielerparty: **4**; mögliche **5** bleibt ungeklärt. Zielrichtung bis ca. **6 Gegner**; keine hart codierte Sonderzahl im Turnloop.
- Jeder Charakter führt seinen Zug unmittelbar aus. Tote Teilnehmer werden übersprungen. Sobald eine Seite keine lebenden Mitglieder besitzt, endet der Kampf.
- Spieler steuert neben dem Hauptcharakter auch die Party-NPCs. Gegner wählen ihre eigenen Aktionen/Ziele.

## 3.2 Dynamische Turnorder — FIXED / IMPLEMENTED

`Speed` bestimmt die Reihenfolge, **nicht** die Zahl regulärer Züge. Vor jedem nächsten Zug wird unter den noch nicht handelnden berechtigten Charakteren nach aktueller Speed neu sortiert; Haste/Slow können dadurch die **noch ausstehenden** Züge einer laufenden Runde beeinflussen. Bereits Handelnde bekommen dadurch keinen zweiten Zug.

Zu Rundenbeginn wird eine Teilnehmer-Snapshot-Liste gebildet. Neu hinzugekommene Einheiten können direkt im Kampf existieren bzw. angewählt werden, aber erst ab der nächsten Runde ihren eigenen regulären Zug erhalten. Gleichstände verwenden derzeit die stabile Listenreihenfolge; eine langfristige Tie-Break-Regel ist OPEN.

## 3.3 Action-Flow — FIXED / IMPLEMENTED im Console-Prototyp

`aktiver Charakter → Aktion auswählen → erforderliche Ressourcen/Cooldown prüfen → gültige Ziele bestimmen → Ziele wählen → Aktion ausführen → Zug abschließen`.

**Technisch unmögliche Aktionen** (zu wenig Mana, Cooldown nicht bereit, keine gültigen Ziele) werden vor Ausführung abgewiesen und verbrauchen keinen Zug. **Legale, aber taktisch schlechte Aktionen** bleiben erlaubt: Beispielsweise darf Meditation bei vollen Mana gewählt werden. Das Spiel soll den Spieler nicht vor jeder suboptimalen Entscheidung schützen.

Im aktuellen `PlayerTurn()` sind Mana-, Cooldown- und Empty-Targets-Checks eingebaut. Eine universelle `ValidateAction`-Architektur ist für Combat-v1 **nicht** vorgesehen. Ungültige Console-Zahlen oder Menüeingaben gehören zum späteren Presentation-/Input-Adapter, nicht zu einer großen neuen Core-Validation-Schicht.

## 3.4 Targeting: Wen? — FIXED / IMPLEMENTED

**Standard ist `TargetType.Any`:** normale Angriffe, gewöhnliche Abilities und spätere Items dürfen jedes **gültige lebende** Ziel wählen — Feind, Verbündete oder sich selbst — sofern die konkrete Aktion nichts einschränkt. Das unterstützt spätere Zombie-/Heal-Inversionen, absichtliche Friendly-Fire- und ungewöhnliche Buff-Synergien.

| TargetType | Regel |
|---|---|
| `Any` | alle lebenden Teilnehmer beider Seiten einschließlich Self |
| `Self` | nur der Anwender |
| `Ally` | eigene Seite einschließlich Self, lebend |
| `Enemy` | gegnerische Seite, lebend |
| `DeadAlly` | tote Mitglieder der eigenen Seite; für spätere Wiederbelebung |

Derzeit unterscheidet der Code genau **zwei Seiten** anhand `Enemy` vs. Nicht-`Enemy`. Factions/Teams für komplexere Szenarien sind spätere Erweiterung, kein Combat-v1-Blocker. Enemy-Normal-Attacks wählen aktuell lebende Spielercharaktere; erweiterte Enemy-AI/Targets sind BACKLOG.

## 3.5 TargetCount: Wie viele? — FIXED / IMPLEMENTED

`TargetType` legt **wer** gültig ist fest; `TargetCount` legt **wie viele** dieser gültigen Ziele eine Aktion auswählt. `1` = ein Ziel; `2`, `3`, … = bis zu so viele **verschiedene** gültige Ziele (gedeckelt durch deren Anzahl); `0` = **alle** gültigen Ziele. Kein Spezialwert wie `6 = alle`. Eine Liste von Zielen wird an `Ability.Execute(Character user, List<Character> targets)` (hier als schematische Signatur; im aktuellen C#-Code sind Parameternamen noch teilweise großgeschrieben) übergeben. Einzelzielskills dürfen intern `targets[0]` verwenden, wenn die zentrale Auswahl die Nicht-Leer-Bedingung sichert.

Mehrfachauswahl (`TargetCount > 1`) wurde mit einer temporären Test-AOE erfolgreich ausprobiert; dieser Testskill wurde anschließend gelöscht. Der `0`-Fall folgt dem implementierten Codepfad, wurde aber noch nicht separat mit einem echten All-Target-Skill funktional getestet. Aktuell verwenden normale Angriffe weiter genau ein Ziel.

## 3.6 Status: Lebenszyklus und Zeit — FIXED / IMPLEMENTED

`Duration = N` bedeutet **N zukünftige eigene Züge des betroffenen Charakters**; der eigene Zug, in dem ein neuer Effekt erzeugt wird, zählt nicht sofort mit. Zu Beginn eines Zugs wird die Liste der **bereits zu Zugbeginn aktiven Effekte** gespeichert. Start-of-Turn-Logik wird aufgerufen; am Ende des Zugs werden nur die zu Zugbeginn vorhandenen Effekte verarbeitet und heruntergezählt. Ein Stun verhindert die eigentliche Aktion, aber nicht Start-/End-of-Turn-Effekte, Cooldown-Ablauf oder den Verbrauch dieses regulären Zugs.

Gleicher **konkreter Effekttyp** stackt nicht mehrfach: erneutes Anwenden setzt `Duration = max(alte Dauer, neue Dauer)`; es triggert `OnApply` nicht erneut. Unterschiedliche konkrete Typen dürfen parallel bestehen. Für persistente Status-Effekte ist eine sinnvolle positive Dauer vorzusehen; One-Shot-Effekte brauchen keinen gespeicherten `Duration = 0`-Status.

**On-hit-Regel:** relevante Hits/Bedingungen müssen erfolgreich sein, bevor OnHit/OnCrit-Effekte angewendet werden. Dodge verhindert diese Folgeeffekte, Block zählt weiterhin als Treffer.

**Aktuelle Effekte:** Stun, Slow, Haste, IronGuard, Poison/Bleed als DoT, Regeneration als HoT. Die Snapshot-Semantik ist eingebaut; komplexe Reapply-/Remove-Kantenfälle gehören in den finalen Regressionstest.

## 3.7 Cooldowns — FIXED / IMPLEMENTED

Cooldowns werden **zu Beginn des eigenen Zugs** heruntergezählt. `Cooldown = 2` bedeutet: die **nächsten zwei eigenen Züge** kann die Fähigkeit nicht erneut benutzt werden, am dritten ist sie wieder verfügbar. Deshalb setzt `StartCooldown()` aktuell `RemainingCooldown = Cooldown + 1`. Dieses `+1` ist weiterhin nötig und **unabhängig** vom gestern korrigierten Status-Dauer-Snapshot.

## 3.8 Damage-Pipeline — FIXED / IMPLEMENTED

`NormalAttack()` bzw. `MagicAttack()` liefert einen `DamageResult` mit DamageType/RawDamage/Crit-Information. `Character.DamageTaken()` verarbeitet für direkte Treffer sinngemäß:

1. Dodge-Prüfung: bei Dodge 0 Schaden und kein erfolgreicher Crit.
2. Block-Prüfung: aktuell halber eingehender Schaden; bei erfolgreichem Hit können Crit und Block koexistieren.
3. Defense/Armor-Reduktion **nur bei Physical**.
4. Jeder aktive StatusEffect darf über `ModifyIncomingDamage(damage, damageType)` den Schaden verändern. Standardmäßig unverändert; `IronGuardEffect` multipliziert **Physical mit 0,7**.
5. DamageType-spezifische Resistance/Weakness anwenden; am Ende runden, nichtnegativ begrenzen und HP aktualisieren.

**DamageTypes derzeit:** Physical, Fire, Ice, Lightning, Holy, Dark. Der Wert `MagicalDamage` ist eine Offensivskalierung, kein finales Element. Separater physischer und magischer Crit sind implementiert. Aktuell können auch magische Hits geblockt werden; ob das auf Dauer so bleiben soll, ist **OPEN**.

**Resistances:** fehlender Eintrag = 0 %; +50 % = halber Schaden, +100 % = immun; −50 % = 150 % Schaden, −100 % = 200 %; Werte werden momentan auf −100…+100 begrenzt. Genaue Balance ist noch nicht final.

**Bewusste Ausnahme:** Die aktuellen Poison-/Bleed-DoTs ziehen direkt HP ab und durchlaufen nicht automatisch Dodge, Block, Defense oder Resistance. Das ist **derzeit beabsichtigte Semantik**, keine automatisch zu behebende Architektur-Schuld. Falls später DoT-Resistances, Immunität oder neue Schutzregeln gewollt sind, wird das als eigene Designentscheidung festgelegt.

**TO VERIFY:** Der generalized IronGuard-Hook ist eingebaut und der Buff kann einem anderen Target verliehen werden; ein isolierter numerischer Test (Physical −30 % nach sonstiger Mitigation, Magic unverändert) ist noch offen. Der vorherige direkte `IronGuardEffect`-Sonderfall in `DamageTaken()` wurde entfernt.

## 3.9 Heilungsanzeige — FIXED

Heilung und HoT sollen **die berechnete Stärke der Heilung** anzeigen, auch wenn dem Ziel weniger HP fehlen; HP selbst bleibt bei `MaxHealth` gedeckelt. Beispiel: 98/100 HP, Heal-Stärke 8 → Anzeige `heals 8`, tatsächlicher Endstand 100/100. Das ist eine bewusste Informationsregel und **kein** HoT-Reporting-Bug. Die UI sollte nominale Heilung und verbleibende HP nicht verwechseln. **Codeabweichung:** Im letzten verifizierten `MinorHeal.Execute()` wird noch `actualHeal` ausgegeben; die nominale Anzeige ist dort noch nicht umgesetzt (siehe § 13.5).


## 3.10 Weitere verbindliche Combat-Semantik — FIXED / OPEN

**Treffer und Folgeeffekte:** Ein Dodge negiert den Treffer und entsprechend dessen OnHit-/OnCrit-Folgeeffekte. Block halbiert im aktuellen Prototyp den direkten Schaden, zählt aber als erfolgreicher Treffer. Ein Crit-Roll vor einem Dodge darf nach Dodge nicht als erfolgreicher Crit ausgegeben werden. Ob spezielle zukünftige Skills Dodge, Block oder andere Schritte bewusst umgehen, muss die konkrete Fähigkeit festlegen.

**Tempo und Aktionshäufigkeit:** Die reguläre Speed-Regel betrifft allein die Reihenfolge der noch ausstehenden Züge. Haste/Slow, eine höhere Speed-Stat oder ein Gleichstand geben nicht stillschweigend zusätzliche reguläre Züge. Extra-Turns können ein späteres spezielles Mechanikpaket werden; ihre Wechselwirkung mit Cooldowns und Status-Dauer ist **OPEN**.

**Runden-Snapshot und Spawns:** Teilnehmer, die während einer Runde hinzukommen, dürfen sofort Ziele werden, aber erhalten ihren ersten regulären Zug erst in der Folgerunde. Bereits tote Einheiten werden bei der Auswahl des nächsten Handelnden übersprungen. Der Kampf endet, wenn keine lebenden Einheiten einer Seite mehr existieren. Ein kommendes Team-/Faction-System oder frei steuerbare beschworene Einheiten darf diese aktuelle Zwei-Seiten-Regel nicht rückwirkend als bereits implementiert darstellen.

**Status-Refresh:** Gleicher konkreter C#-Statustyp wird verlängert statt doppelt gestackt; `OnApply` wird bei diesem Refresh derzeit nicht erneut ausgeführt. Die aktuelle Regel `max(alte, neue Dauer)` gilt unabhängig davon, ob die neu angebotene Dauer kleiner ist. Unabhängige Status-Untertypen wie hypothetisches WeakPoison und HeavyPoison könnten gleichzeitig bestehen, sofern sie später tatsächlich als unterschiedliche Typen modelliert werden.

**Dauer:** N zukünftige eigene Züge ab erfolgreicher Neuanwendung. Der Snapshot hält neu erzeugte Effekte von einem ungewollten sofortigen Ablauf am Ende des laufenden Zugs ab. In besonderen Fällen (Refresh eines bereits im Snapshot enthaltenen Effekts, Entfernung während Start-of-Turn, Target stirbt durch einen Tick) wird das tatsächliche Verhalten im Regressionstest geprüft, statt eine neue Regel allein aus dem Kommentar abzuleiten.

**Aktion und Validität:** `ManaCost`, `RemainingCooldown`, `TargetType` und `TargetCount` sind Core-Regeln. Ausgewählte Ziele müssen zur angefragten Aktion passen; die aktuelle synchrone Console-Auswahl liefert sie direkt aus `GetValidTargets`. Für die spätere zeitversetzte Unity-Auswahl ist vor dem tatsächlichen Ausführen erneut zu entscheiden, wie veraltete oder inzwischen tote Ziele behandelt werden. Das ist eine **Integrationsentscheidung**, kein Auftrag für einen umfangreichen `ValidateAction`-Framework-Bau vor Unity.

**Items und Flee:** Die Menüeinträge existieren gegenwärtig nur als Console-Platzhalter. Es gibt noch keine verbindliche Item-/Fluchtmechanik im aktuellen Combat-Prototyp. Wiederbelebung mit `DeadAlly` ist Targeting-Vorbereitung, **kein** Beleg für einen vorhandenen Revive-Skill.

---

# 4. Elemente und Wechselwirkungen

**DIRECTION:** Elemente müssen später mehr sein als verschieden gefärbter Schaden: Fire eher Damage/Burn; Ice eher Slow/Freeze/Control; Lightning Tempo/Stun/Multi-Hit; Holy Support/Heilung; Dark Drain/Debuffs/ungewöhnliche Interaktionen. Die genauen Multiplikatoren und Statuschancen sind OPEN und nicht aus diesen Archetypen abzuleiten.

**FIXED:** Der technische Schadenstyp und Resistances sind zentral, nicht in jeden Skill kopiert. Eine spätere Zombie-/Trickster-Inversion kann Heilung/Schaden, Elemente oder Zielprioritäten verändern, muss nach Definition aber intern konsistent sein. Die besondere freie `Any`-Zielauswahl wird dafür bewusst beibehalten.


## 4.1 Element-Profile als Designrichtung — DIRECTION, keine finalen Formeln

| Typ / Familie | Angestrebtes Profil | Noch offen |
|---|---|---|
| `Physical` | Direkte Weapon-/Strength-Interaktion; physische Mitigation und waffenspezifische Skills | Armor-/Defense-Balance, Sondertreffer, zusätzliche Physik-Effekte |
| `Fire` | Eher starker Schaden, Burn oder anderer DoT; tendenziell weniger Kontrolle | Ob jede Fire-Ability einen DoT erhält, Chancen und Koeffizienten |
| `Ice` | Eher Slow, Freeze oder andere Control-Tools | Freeze-Logik, genaue Multiplikatoren und Immunitäten |
| `Lightning` | Tempo, Stun, mögliche Mehrfachtreffer | Echte Extra-Turn-Interaktion und Statuschancen |
| `Holy` | Heilung, Schutz, Support und thematisch passende offensive Skills | Ob und wie offensive Holy-Builds skaliert werden |
| `Dark` | Drain, Debuffs, riskante Ressourcen- oder Inversionsinteraktionen | Exakte Kosten, Elemente-Antworten und Synergien |

Diese Tabelle fixiert **keine** automatischen Nebeneffekte für alle Angriffe eines Typs. Ein Fire-Skill kann schlicht Fire-Damage verursachen, ohne dass das gesamte Element-System bereits fertig ist. Neue Elemente und konkrete Schadensformeln sind OPEN.

## 4.2 Resistance- und Damage-Informationsregel — FIXED

`MagicalDamage` ist ein Offensivwert; die elementare **Art** eines Treffers wird über `DamageType` getragen. Fehlende Resistance bedeutet im aktuellen Combat `0`. Positive Resistance vermindert eingehenden Schaden, negative bedeutet Weakness und erhöht ihn. Die zentrale direkte-Treffer-Pipeline ist die Referenz für weitere Damage-Typen, soweit ihre Regel explizit dorthin gehören soll.

DoTs sind die **bewusste, dokumentierte Ausnahme**: Poison und Bleed benutzen gegenwärtig direkten HP-Abzug. Künftige Schadensquellen dürfen weder stillschweigend als DoT noch als normaler direkter Treffer kategorisiert werden, ohne ihren beabsichtigten Regelweg zu definieren.

---
# 5. Party, Roster und Companions

## 5.1 Aktive Party und Hauptfigur — FIXED / DIRECTION

Die Hauptfigur gehört im Normalfall in die aktive Party; ausdrücklich inszenierte Story-/Gameplay-Ausnahmen sind möglich. Die Zielgröße beträgt **vier aktive Charaktere**; eine Variante mit fünf wurde erwogen, aber nicht beschlossen. Ein langfristiges **Roster von ungefähr acht Charakteren** ist als Größenordnung im Gespräch, die endgültige Rostergröße ist OPEN. Alle aktiven Party-Mitglieder werden im Kampf vom Spieler gesteuert, unabhängig von ihrer Persönlichkeit in der Story.

Die Party soll nicht nur vier allmächtige Favoriten sein, sondern ein Satz spezialisierter Werkzeuge. Ein sonst selten eingesetzter Charakter darf bei einem bestimmten Boss plötzlich zentral werden. Healing, Frontline, Physical/Magic, Elementabdeckung, Burst, Sustain, Kontrolle, Statusantworten und Resistances bilden unterschiedliche Vorbereitungsmöglichkeiten.

## 5.2 Verschiedene Companion-Freiheiten — FIXED

- **Starter-/Core-Charaktere:** vollständiger Entwicklungsweg und weitreichende Spielerentscheidung über spätere Spezialisierungen.
- **Ausgewählte Story-Companions:** einzelne oder alle früheren Entwicklungsentscheidungen sind Teil ihrer Vorgeschichte und bleiben fest; weitere Feinabstimmung kann möglich sein.
- **Spätere Recruits:** können bereits mit einer Zwischenklasse ins Roster kommen. Ihr bisheriger Pfad steht fest, ihre verbleibende Spezialisierung kann teilweise vom Spieler bestimmt werden.

Merksatz: *Die Vergangenheit eines Companions ist fest; seine Zukunft kann teilweise vom Spieler geformt werden.* Ob die Klasse einzelner Companions abhängig von der Main-Class variiert, bleibt OPEN. Es darf auch companion-exklusive Special Classes geben.


## 5.3 Roster als langfristiger Encounter-Werkzeugkasten — FIXED

Ein Companionsystem ist nicht nur ein Sammelbildschirm für die vier stärksten Figuren. Mit einem breiteren Roster soll der Spieler bewusst zwischen Healing, defensiver Stabilität, Physical Damage, Element-/Magic-Abdeckung, Burst, Sustain, Kontrolle, Cleanse/Status-Antworten und anderen Utility-Werkzeugen planen können. Eine Figur, die auf vielen normalen Maps kaum eingesetzt wird, darf für einen optionalen Encounter einen entscheidenden Beitrag leisten.

Die Hauptfigur nimmt normalerweise einen der aktiven Plätze ein und beeinflusst damit die übrige Partyplanung. Ausgewählte Storysequenzen dürfen davon ausdrücklich abweichen. Storypersönlichkeit und Combat-Steuerung sind voneinander getrennt: Begleiter bleiben erzählerisch eigenständige Figuren, der Spieler steuert im Kampf ihre normalen Aktionen.

## 5.4 Companions: vergangene und zukünftige Entscheidungen — FIXED / OPEN

Vollständig formbare Starter-/Core-Companions können ihren Jobpfad in erheblichem Umfang selbst wählen. Einige storyrelevante Gefährten bringen dagegen einen festeren Pfad aus ihrer Vergangenheit mit. Später hinzukommende reguläre Recruits können schon eine Zwischenklasse besitzen, bei der der Spieler nur über die noch offenen späteren Entwicklungen verfügt.

**Offen** ist, ob der gewählte Hauptcharakter-Job bei einzelnen Companions die Auftretensklasse beeinflusst, ob alle Rekrutierungen in jedem Run möglich sind und welche Figuren gegebenenfalls exklusive Sonderklassen besitzen. Keine dieser Optionen darf als bereits beschlossener vollständiger Companion-Plan ausgegeben werden.

---

# 6. Jobs und Klassenentwicklung

## 6.1 Neues Zielgerüst — DIRECTION, noch nicht implementiert

**4 Starterklassen → jede mit 2 Zwischenklassen → jede Zwischenklasse mit 2 Endklassen = 16 Endjobs.** Das ist das aktuelle Planungsmodell und ersetzt die frühere GDD-Aussage, Anzahl der Base Classes und der vierte Knight-Pfad seien noch völlig offen. **Die genauen Verbindungen zwischen den einzelnen Namen sind weiterhin OPEN**; die folgende Tabelle ist eine Sammlung von vorgesehenen Endjobs nach Rollenfamilie, **kein verbindlicher Skilltree**.

| Rollenfamilie | Aktuell vorgesehene Endjob-Namen |
|---|---|
| Physical / Martial | Assassin, Ranger, Samurai, Monk |
| Tank / Knight | Paladin, Templar, Gladiator, Dark Knight |
| Healing / Support | Priest, Reaper, Cleric, Chanter |
| Magic / Hybrid | Sorcerer, Summoner, Battle Mage, Ghostblade |

Die vier C#-Testklassen **Warrior, Rogue, Cleric und Mage** sind derzeitige Combat-Prototypen. Ihre Dateinamen und vier Testskills sind **nicht** automatisch die endgültigen Namen, exakten Starterrollen oder Levelpfade des späteren Spiels. Insbesondere kann `Cleric` aktuell Test-Basisklasse und später zugleich Name eines geplanten Endjobs sein; das muss vor der finalen Klassenimplementierung sauber entschieden werden.

## 6.2 Endjob-Identitäten — DIRECTION mit offenen Details

**Physical:** Assassin steht für spezialisierten Single-Target-Burst, Crit, Bleed/Poison, Multi-Hit und potenzielle Tempo-/Execute-Synergien; Ranger für die eigene fernkampforientierte Rolle; Samurai für einen klaren waffen- und timingzentrierten Kampfpfad; Monk für eine eigenständige Nahkampfidentität, deren genauer Loop noch nicht feststeht. Frühere **Ninja**-Überlegungen sind nicht automatisch ein zusätzlicher 17. Endjob und werden nur bei bewusster späterer Neuentscheidung wieder aufgenommen.

**Tank:** Paladin ist ein stabiler Holy-/Physical-orientierter Tank; Templar eine offensivere, standfeste Knight-Variante mit Greatsword-/Holy-Richtung; Dark Knight verbindet Physical/Dark mit riskanter HP-Nutzung als möglichem Ressourcen-/Defensive-Loop. Gladiator ersetzt die alte offene vierte Knight-Namenslücke; seine konkrete mechanische Identität ist noch OPEN.

**Healing/Support:** Priest ist stärkerer klassischer Caster-/Healer; Cleric der defensive Healer mit Schutz/Sustain und nach bisheriger Konzeptidee One-Hand + Shield statt Staff; Chanter ein offensiver Support-/Buff-Hybrid mit Combat-Staff-Richtung. **Reaper** ist der jüngere Name für einen eigenen vierten Pfad; exakte Spielweise und Abgrenzung zu Holy/Dark-Hybriden sind OPEN. Die vier Jobs dürfen sich einige Basis-Heals teilen, sollen aber durch exklusive Werkzeuge verschieden bleiben.

**Magic/Hybrid:** Sorcerer steht als Richtung für spezialisierte offensive Magie; Summoner für einen eigenständigen Beschwörungs-Loop, dessen Turn-/Summon-Regeln noch nicht definiert sind. Battle Mage kombiniert physische und elementare Anteile, eventuell Combat Staff/Chain-ähnliche Rüstung. Ghostblade ist ein magisch-physischer Schwert-/Mystic-Knight-artiger Pfad; genaue Elementbindung und Waffenregeln sind OPEN. Kein neuer Summon- oder Ghostblade-Fundamentalmechanismus wird vor Unity begonnen.

Diese Beschreibungen sind **Arbeitsprofile**, keine zugesagten Ability-Listen, festen Waffenrestriktionen oder Balancewerte. Frühere Ideen für Berserker, Ninja, Hunter/Beastmaster/Tamer und Fencer/Parry bleiben bei Bedarf im **separaten Backlog**, nicht heimlich im neuen 16er-Baum.

## 6.3 Was Spezialisierung bedeutet — FIXED

Eine Klassenwahl verändert nicht bloß Stats. Sie kann neue Waffen/Rüstungen, Abilities, Ressourcen und Synergien freigeben und ältere Optionen entziehen. Je weiter der Pfad, desto schärfer darf die Identität und desto enger der Werkzeugkasten werden. Verwandte Endjobs dürfen gemeinsame Skills behalten, ohne dieselbe Spielweise zu besitzen.

**DIRECTION:** Beim Entwurf rückwärts vorgehen: Endjob-Kampffantasie → Waffen/Rüstung → Ressourcen/Abilities → bewusste Kosten → passende Zwischen- und Starterklasse. Die früher diskutierten Levelmarken um **35** und **70** sind Beispiele, keine aktuellen Fixwerte. Ein bewusster Verbleib auf einer früheren Klassenstufe darf einen eigenen langfristigen Weg darstellen; wie dieser mit dem neuen 16-Endjob-Modell und dem Content-Budget zusammenpasst, ist noch auszuarbeiten.

## 6.4 Talente, Ability-Freischaltung und Respec — FIXED / DIRECTION / OPEN

**FIXED:** Der Combat-Core muss nur wissen, welche Abilities ein Charakter **besitzt/ausgerüstet hat**; woher sie stammen, ist Verantwortung von Progression/Content. Freischaltungen können aus Leveln, Quests, Trainern, Scrolls, Drops, Exploration, Mastery, Spezialisierung und versteckten Bedingungen kommen. Nicht jede Ability ist jedem Charakter zugänglich.

**DIRECTION:** Talentpunkte sind **knapp und bedeutsam**, nicht beliebig überall verteilbar. Ein Talent-/Ability-Netz kann Knoten, Cluster, versteckte Voraussetzungen und Job-Gates enthalten. Nicht mehr verfügbare Punkte aus durch Spezialisierung ausgeschlossenen Abilities sollen zurückerstattet werden. Es ist attraktiv, **mehr Skills zu lernen, als gleichzeitig ausgerüstet werden können**; etwa **sechs aktive Ability-Slots** wurden als Zielgröße diskutiert, aber nicht abschließend bestätigt.

**DIRECTION:** Während der Story ungefähr **ein bis zwei begrenzte Respec-Möglichkeiten**, spät bzw. im Postgame ein freier vollständiger Respec. Zeitpunkt, Kosten, Auswirkungen auf irreversible Story-Flags und Verfügbarkeit für Companions sind OPEN; Storyentscheidungen werden dadurch nicht automatisch rückgängig gemacht.

**OPEN:** Exakte Punktvergabe, konkrete Talentbaum-Topologie, Talent Tree vs. Ability Constellation, Use-to-Level-Mastery (Spam-Exploit vermeiden), Endstufen-/Mastery-Pfade und Reihenfolge der Klassen-Upgrades.


## 6.5 Endjobs: erhaltene Detailprofile — DIRECTION / OPEN

Die folgende Tabelle bewahrt die bisherige Rollenrichtung **ohne einen nicht beschlossenen Klassenbaum zu erfinden**. Jedes Profil ist eine Designskizze; fehlende Waffen, Ressourcen, Skills, Aufstiegslevel und direkte Vorgänger sind weiterhin OPEN.

| Vorgesehener Endjob | Gesicherte / besprochene Richtung | Nicht entschieden |
|---|---|---|
| **Assassin** | stark spezialisierter Dagger-/Single-Target-Burst; Crit, Poison/Bleed, Multi-Hit, Execution; besondere Tempo-/Extra-Turn-Synergien denkbar | genaue Ressourcen, Bedingungen, Waffenanzahl und Stärke |
| **Ranger** | eigenständige fernkampforientierte Physical-Rolle; soll nicht nur ein Assassin aus Distanz sein | Waffenpool, Munition/Markierung, AoE-/Single-Target-Verhältnis |
| **Samurai** | eigenständige, waffen- und Timing-bezogene Martial-Rolle | genaue Ressource, Cast-/Charge-Regeln und Ausrüstungsgrenzen |
| **Monk** | klarer Nahkampf-Job mit eigener mechanischer Identität; frühere Vorschläge um offensive/defensive Martial-Wege sind Ideen, nicht der feststehende Baum | Core-Loop, Gegenschlag, Haltungen, Ressource |
| **Paladin** | defensiver stabiler Knight-/Tank-Pfad; Physical und Holy, Schutzwerkzeuge | Shield-/Waffenkatalog, Defensive-Kosten und konkrete Holy-Skills |
| **Templar** | robust, offensiver als Paladin, Greatsword-/Holy-Richtung; keine bloße Paladin-Kopie mit mehr Schaden | genaue Rüstung und defensiver/offensiver Wechsel |
| **Gladiator** | vorgesehene eigenständige Tank-/Martial-Endklasse als vierter Knight-Namensplatz | Abgrenzung zum Paladin/Templar/Dark Knight, Weapon-Loop, Ressourcen |
| **Dark Knight** | Physical + Dark, standfest mit anderer Defensive; HP als riskante mögliche Ressource | exakte HP-Schwellen und Selbstschutzmechaniken |
| **Priest** | klassischer offensiv/defensiv abstimmbarer Caster-/Healer mit eigenen exklusiven Tools | Focus-/Buch-/Relic-/Orb-Ausrüstung und Skill-Balance |
| **Reaper** | eigener Endjob im Support-/Healing-Cluster; jüngere Namensentscheidung ersetzt frühere offene vierte Rolle | Verhältnis von Healing, Damage, Dark und Drain; exakte Klasse/Funktion |
| **Cleric** | defensiver Healer, Sustain und Schutz; früheres Konzept One-Hand + Shield, kein Staff | ob Name und konkrete Waffenrestriktionen final bleiben; Verwechslungsfreiheit mit C#-Testklasse |
| **Chanter** | offensiver Support-/Holy-Hybrid, Buffs und Utility; Combat Staff als starke bisherige Richtung | genaue Support-/Healing-Grenzen und Mechaniknamen |
| **Sorcerer** | stärker offensiv spezialisierter magischer Pfad mit Elementidentität | Elementzugang, Spell-/Cast-Ressourcen, Ausrüstungsrestriktionen |
| **Summoner** | eigenständiger Beschwörungs-/Partner-Loop statt identischer Standard-Caster | Spawn-/Turn-/Target-/Party-Regeln und Begrenzungen |
| **Battle Mage** | echter Physical-Element-Hybrid; Angriffe können beide Komponenten verbinden; Combat Staff und Chain-artige Armor als ältere Richtung | endgültiger Weapon-/Armor-Pool und hybride Skalierung |
| **Ghostblade** | magisch-physische Schwert-/Mystic-Knight-artige Interaktion, Weapon- und Element-Synergien | Status-, Infusion-, Ressourcen- und genaue Damage-Type-Regeln |

**Keine stillschweigende Erweiterung:** Die älteren Ideen `Ninja`, `Berserker`, `Hunter`, `Beastmaster/Tamer`, `Fencer` und `Warrior/Vanguard` sind **nicht** automatisch weitere Endjobs in diesem 16er-Modell. Ihre erhaltenen Detailkonzepte stehen im Legacy-/Ideenarchiv (§ 15.3). Eine spätere Umbenennung, Zusammenlegung oder Neubewertung ist möglich, aber muss dokumentiert werden.

## 6.6 Spezialisierung, Fähigkeitsverlust und alternative Meisterschaft — FIXED / OPEN

Spezialisierung ist eine Entscheidung über **zugängliche Spielweise**, nicht eine reine Level- oder Prozent-Steigerung: ein neuer Job kann Waffen, Armor-Kategorien, Ability-Voraussetzungen und Ressourcen eröffnen und zugleich frühere Wege schließen. Ein Skill darf über seinen früheren Waffen-/Shield-Requirement unbrauchbar werden. Die dadurch entwerteten investierten Ability-Punkte sollen nach der besprochenen Richtung zurückgegeben werden; die genauen Bedingungen sind noch zu spezifizieren.

Frühere Klassenstufen können **grundsätzlich** bewusst als eigenständige Endgame-Alternativen interessant sein: Ein Charakter könnte auf einer Basisklasse oder Zwischenklasse bleiben und dort eine andere Mastery-/Gear-Identität entwickeln. Ob und in welcher Anzahl das zusätzlich zu den 16 geplanten Endjobs produktionsfähig ist, ist **OPEN**. Der Wunsch begründet nicht automatisch 4 + 8 zusätzliche vollständige Endjobs.

Bei der Jobkonzeption wird möglichst **rückwärts** gearbeitet: gewünschter finaler Gameplay-Loop → passende Waffe/Rüstung → Fähigkeiten/Mechaniken → bewusste Nachteile/Ausschlüsse → gemeinsamer Vorgänger. So bleiben Zwischenklassen sinnvolle Knoten statt bloßer Übergangszeiten. Früh genannte Schwellen um Level 35 und 70 sind nur illustrative Planungswerte.

## 6.7 Ability Network / Talent Tree / Mastery — DIRECTION / OPEN

Die frühere Visualisierungsrichtung für Ability-Fortschritt ist ein **zweidimensionales Knoten-/Constellation-Netzwerk** mit zentralem Startpunkt, verbundenen Clustern, optional isolierten/versteckten Knoten und einer verschiebbaren Ansicht. Knoten können aktive Abilities, Passives, Upgrades, Branches oder Spezialisierungs-Gates freischalten. Noch nicht festgelegt ist, ob dieses Netzwerk zugleich der Talent Tree ist oder ob ein eigener Talent Tree bestehende Skills verstärkt, während das Ability Network neue Tools freischaltet.

Mögliche Sichtbarkeitsstufen: **bekannt/sichtbar**, **sichtbar als `?`**, **vollständig verborgen bis zu einem Trigger**. Das Netz soll dem Spieler eine Richtung zeigen können, ohne alle zukünftigen Belohnungen zu verraten. Die früh erwogene Vergabe eines Talentpunkts etwa alle fünf Level ist **kein beschlossener Takt**.

**Abschluss einer Spezialisierung:** Wenn ein Job Skills ausschließt, bleiben Punkte aus gesperrten Skills nicht dauerhaft verloren; eine konkrete Refund-Regel wird beim echten Progressionssystem entworfen. Mehr gelernte als ausgerüstete Skills ist gewünscht; die oft genannte Größe von sechs aktiven Slots ist weiterhin ein **Zielwert, keine festgelegte UI-Regel**.

**Respec:** während der Story begrenzt bzw. an einzelne klare Möglichkeiten gebunden (früher ca. ein bis zwei), im späten/postgame Verlauf freie komplette Re-Spezialisierung als Richtung. Selbst eine freie Skill-/Job-Rücksetzung nimmt **keine** versäumten Quest-, Story- oder World-State-Entscheidungen automatisch zurück. Die genauen Story-Ausnahmen, Kosten, Zeitpunkt und Companion-Regeln sind OPEN.

**Skill-by-use-Mastery** wurde erwogen, aber nicht beschlossen. Ein Design, das wiederholtes Spammen trivialer Gegner optimal belohnt, wäre für dieses Ziel fragwürdig; eine solche Mechanik braucht gegebenenfalls Relevanzbedingungen für Gegner/Situation.

---

# 7. Waffen, Rüstung, Items und Loot

## 7.1 Waffen und Klassengrenzen — FIXED

Waffenpools sollen mit der Klassenprogression schärfer werden. Eine Spezialisierung **darf eine zuvor erlaubte Waffenfamilie entfernen**; dadurch können an Waffen gebundene alte Skills unbenutzbar werden. Abilities dürfen echte Requirements wie Klasse, Spezialisierung, Waffentyp, Schild, Status oder Ressource verlangen. Die konkrete Requirements-Datenstruktur ist BACKLOG.

Mehrere Klassen dürfen dieselbe Waffenfamilie verwenden, sofern ihre Stats, Effekte und Abilities die Builds dennoch unterscheiden. Beispielkonzepte: Battle Mage und Chanter können sich eine Combat-Staff-Familie teilen, ohne dieselbe Rolle zu erfüllen. Armor kann eigene Klasseneffekte und Richtungen haben; konkrete Slot-/Gewicht-/Rüstungstyp-Regeln sind OPEN.

**FIXED:** Gemeinsam tragbares Gear erhält **kein** „für Klasse X empfohlen“-Etikett. Sichtbar sind echte Requirements, Waffentyp, Stats und Effekte; der Spieler entscheidet über den Build. Eine Klassenrestriktion ist eine Regel, keine Meta-Empfehlung.

## 7.2 Build-definierende Ausrüstung — DIRECTION

Ausrüstung soll mehr als lineare Stat-Sticks ermöglichen. Gewünschte Stufen reichen von allgemein nutzbarer Waffe über spezialisierungsgebundenes Gear bis zu verwandten Endklassen geteilten und schließlich klassenexklusiven Signature-/Legendary-Waffen. Seltene, mechanisch starke Effekte dürfen individuelle Build-Kerne eröffnen. Build-definierende Items sollen in ihrer Verfügbarkeit begrenzt und teils einzigartig sein.

**DIRECTION:** Jeder Endjob soll langfristig eine oder mehrere charakteristische seltene Belohnungen besitzen, aber ein Run muss nicht alle erhalten. Genaue Zahl, Quests, Drop-Raten, Erwerbsbedingungen, Socket-/Manastone-System, Crafting, Item-Ökonomie und Ausrüstungs-Slot-Struktur sind OPEN bzw. BACKLOG. Frühere illustrative Drop-Raten oder „ca. vier Legendaries pro Run“ sind **keine festgesetzten Produktionswerte**.

## 7.3 Kein Smart Loot — FIXED

Die Welt verteilt Loot nicht automatisch passend zur aktuellen Party. Ein Save darf eine ausgezeichnete Mage-Waffe finden, obwohl gerade niemand sie tragen kann. Seltene Boss- und Questbelohnungen müssen dem aktuellen Build nicht nützen. Extrem seltene Drops sind als bewusste optionale Belohnungen möglich; austauschbare tausende Stat-Sticks ohne Identität sind nicht das Ziel.

## 7.4 Items und Verbrauchsgüter — DIRECTION / BACKLOG

Normale Items sollen grundsätzlich dieselbe freie Zielphilosophie wie Abilities nutzen (`Any` für lebende Ziele, wenn nicht ausdrücklich eingeschränkt; z. B. Revive `DeadAlly`). Inventar, Verfügbarkeit, Besitzchecks, Shops, Chests, Gear-Anlegen und eine allgemeine Interaktionsvalidierung werden **nicht** in Combat-v1 vorwegimplementiert. Die `Item`-/`Flee`-Menüpunkte des aktuellen Console-Prototyps sind noch Platzhalter.


## 7.5 Waffenprogression, Tier-Logik und Requirement-Beispiele — FIXED / DIRECTION

Die frühere Detaillierung der Ausrüstungsstufen bleibt als Richtung erhalten: (1) allgemein nutzbarer Waffentyp, (2) erst nach einer Progressions-/Spezialisierungsstufe verwendbar, (3) von verwandten Endjobs gemeinsam verwendbares Endgame-Gear, (4) stark individualisierte, klassenexklusive Signature-/Legendary-Ausrüstung. Die konkrete Datenstruktur und numerischen Tier-Grenzen sind OPEN.

**Reines Designbeispiel, kein aktueller Itemkatalog:** Mehrere frühe Klassen dürfen Dagger nutzen; ein späterer Master-Dagger könnte für verwandte Endjobs freigeschaltet sein; eine Assassin-Signature-Waffe wäre dagegen nur für Assassin. Eine Spezialisierung darf einen zuvor verfügbaren Bow oder Staff verlieren. Frühe Skills mit echtem Waffentyp-Requirement können dadurch künftig ausgeschlossen sein.

Sichtbar gemacht werden Item-Typ, Stats, Effekte und **tatsächliche** harte Voraussetzungen. Die UI verteilt für gemeinsam kompatibles Gear keine „Best for X“-Sterne und kein implizites Klassen-Ranking. Selbst wenn Chanter und Battle Mage beide Combat Staff verwenden, bleiben ihre gewünschten sekundären Stats, Effekte und Fähigkeiten unterschiedlich.

## 7.6 Signature Weapons und Loot-Rarität — DIRECTION / OPEN

Langfristig soll jede finale Endklasse mindestens eine markante Signatur-/Legendary-Waffe oder vergleichbare klassenprägende Belohnung besitzen. Ein Legendary soll **eine Spielweise oder Synergie erkennbar verändern**, nicht bloß einen linearen Attack-Wert um einige Punkte erhöhen. Denkbare Beispiele aus der früheren Designphase: Assassin verstärkt Crit/Poison/Tempo; Cleric Protection/Healing/Sustain; Chanter Staff/Buff-Hybrid; ein nicht mehr im aktuellen 16er-Plan verankerter Berserker erhöhte Risk/Reward/Lifesteal. Dies sind **Beispiele**, keine implementierten Unique-Items.

Die Zahl der Legendaries, die in einem Run tatsächlich erreichbar sind, bleibt offen; früh wurden etwa vier pro Run diskutiert, **nicht entschieden**. Seltene Drops bis hin zu illustrativen 0,5 %, 0,1 % oder in extremen optionalen Fällen 0,01 % wurden als grundsätzlich mögliche Philosophie besprochen, aber sind **keine fixen Produktionsquoten**. Grind ist nur sinnvoll, wenn er zu einer bedeutungsvollen individuellen Belohnung führt, nicht zu tausenden austauschbaren Stat-Sticks.

**Kein Smart Loot:** Boss, Welt und Quest vergeben ihre Gegenstände unabhängig davon, welche Klassen die aktuelle Party gewählt hat. Ein Save darf eine seltene, aktuell untragbare Waffe erhalten. Optionale Superboss-Belohnungen müssen nicht für jedes Roster verwertbar sein.

## 7.7 Rüstung, Sockets und Ökonomie — OPEN / BACKLOG

Frühere Ideen beinhalten spezielle Armor-Klassen und gearbasierte Wechselwirkungen, begrenzte einzigartige Build-definierende Gegenstände sowie eventuell ein Aion-artiges Socket-/Manastone-System. Ein solches System wird nur übernommen, wenn es **eigenständige Build-Tiefe** erzeugt; es ist keine Pflicht, Slots mechanisch bloß zu vervielfachen. Exakte Ausrüstungsplätze, Rüstungsgewichtstypen, Accessoires, Crafting, Shops, Economy, Inventarlimits und Item-/Quest-Requirements sind noch offen. Das frühere Console-Bootstrap mit auskommentierten Equipment-Ideen ist **kein verbindlicher Itemkatalog**.

---

# 8. Gegner, Bestiary und Informationsökonomie

## 8.1 Knowledge aus Beobachtung — FIXED

Das Bestiary wird nicht nach erstem Sichtkontakt oder einem Kill automatisch vollständig. Abhängig von tatsächlich beobachteten Ereignissen können Name, HP/Level, Weaknesses, Resistances, Immunities, Abilities, Statusreaktionen und besondere Traits bekannt werden. Beispiel: Eine beobachtete Schadensnull kann eine mögliche Immunität erschließen; eine erstmals eingesetzte Bossfähigkeit wird als Name/Beobachtung vermerkt, ihre Erklärung ggf. erst später erweitert.

Repeatable Enemies dürfen lange teilweise unbekannt bleiben; genaue Freischaltregeln sind OPEN. Für wirklich einmalige oder dauerhaft missbare Gegner soll nach dem **endgültigen Sieg** der verfügbare Bestiary-Eintrag vollständig freigeschaltet werden, damit Wissen nicht unwiederbringlich verloren geht. Ein nur scheinbar einmaliger, später wiederholbarer Gegner erfordert eine klare Datenregel.

## 8.2 Beschreibungen vs. Lösungen — FIXED

Bekannte Abilities und Effekte brauchen spielrelevante, konsistente Aussagen; exakte interne Multiplikatoren/Chancen müssen nicht immer im Tooltip stehen. **Definierte Dauer und relevantes Turn-Timing** sollen grundsätzlich erkennbar sein, sobald die Fähigkeit bekannt/beschrieben ist. Beobachtbare Reaktionen, Reihenfolgeänderungen, 0 Damage, Reflect oder Heal-Inversion sind Teil des Lernens. Das Spiel erklärt **Werkzeuge**, aber nicht automatisch die Bosslösung.

Das Knowledge-System ist ein von Combat getrenntes System: Die Welt-/Enemy-Daten beschreiben, **was wahr ist**, Knowledge speichert, **was der Spieler erfahren hat**, und UI entscheidet, **was davon sichtbar wird**. UI-Verstecken darf nicht die tatsächliche Combat-Regel verändern.


## 8.3 Welche Informationen durch welches Ereignis bekannt werden — FIXED CONCEPT / OPEN FORMULAS

Der Bestiary-Eintrag kann stufenweise Name, Level, HP, effektive Schadensarten, Schwächen, Resistances, Immunitäten, sichtbare Abilities, deren Beschreibungen und Sondermerkmale zeigen. Der Spieler entdeckt die Werte **durch relevante Begegnungen**, nicht indem ein Datenbank-Datensatz automatisch vollständig geöffnet wird. Beispiele: Ein beobachteter Physical-Hit mit null Schaden kann eine Hypothese bzw. je nach Regel eine dokumentierte Immunität freischalten; nach einem gesehenen Boss-Skill wird zunächst dessen Name und später seine ausführlichere Beschreibung bekannt.

Ein wiederholbar bekämpfbarer Gegner darf nach einem Kill weiterhin unbekannte Felder haben. Die **exakten** Ereignisse, Schwellen, Wiederholungszahlen und Informationsstufen sind noch nicht festgelegt. Ein endgültig besiegter **wirklich einmaliger bzw. dauerhaft missbarer** Gegner soll dagegen seinen verfügbaren Bestiary-Eintrag nach dem letzten möglichen Encounter vollständig offenbaren. Das ist eine Belohnung für den Sieg und vermeidet dauerhaft unmögliche Wissensvervollständigung. Die Ausnahme gilt nicht automatisch für jeden Storyboss, der später wiederholbar ist.

## 8.4 UI-Wissensgrenzen und Ability-Beschreibungen — FIXED

Die UI darf bei unbekannten Daten `???` zeigen oder Informationen verbergen, ohne dass dadurch die **wahren Enemy- oder Combat-Daten** verändert werden. Welt-/Enemy-Definition, vom Spieler freigeschaltetes Knowledge und Präsentation sind drei verschiedene Verantwortlichkeiten.

Der volle Beschreibungstext einer bekannten Ability muss für Entscheidungen relevante Bedingungen sowie definierte **Dauer und Timing** verständlich machen, aber keinen Sourcecode, jeden Zufalls-Seed oder sämtliche Multiplikatoren offenlegen. Bei besonders komplexen, einmaligen Bossen darf nach vollständigem Sieg auch eine bisher verborgene logische Triggerregel im Bestiary erklärt werden. Ein Tooltip soll jedoch keine automatische Encounter-Lösung aus den eigenen Tools zusammenstellen.

---

# 9. Boss- und Encounter-Design

## 9.1 Lernbare, überraschende Mechaniken — FIXED

Bossmechaniken müssen nicht vor dem ersten Pull offengelegt werden. Gute Begegnungen kombinieren bekannte Archetypen mit ein bis zwei ungewohnten Interaktionen. Beispielsweise darf ein defensiver Knight-Boss überraschend Reflect, eine verzögerte Aktion oder veränderte Resistances erhalten. Überraschungen sollen durch Ergebnisse, Animationen, Status oder wiederholte Versuche beobachtbar und intern konsistent sein.

## 9.2 Trickster / Inversion — DIRECTION / BACKLOG

Eine Kefka-inspirierte Trickster-Begegnung ist als Designrichtung gewünscht. Denkbare Effekte sind zeitweise Heal→Damage, Physical Damage→Healing, Fire↔Ice oder andere definierte Inversionen. Bedingungen, Ankündigungen, Verzögerung, erlaubte Zieltypen, Dauer und genaue Interaktionsregeln bleiben OPEN. Die alte GDD nannte zehn Turns als **Beispiel**, nicht als beschlossene finale Zahl. Dieses System wird nicht vor dem ersten Unity-Kampf implementiert.

## 9.3 Persistente Encounter-Flags — FIXED

Frühere Aktionen in der Welt können Bosse verändern: ein anderer Killcount, übersehene Quest oder eine Entscheidung kann Ausrüstung, Phase oder Aggressivität ändern. Ebenso darf eine frühe Bossphase Flags für spätere Phasen hinterlassen; eine später richtig gelöste Mechanik muss Fehler früherer Phasen nicht rückwirkend löschen. Konkrete Encounter und Schwellenwerte bleiben EXAMPLE/OPEN.

## 9.4 Checks und Build-Antworten — FIXED

Optionaler Content darf bestimmte Damage-/Statusprofile verlangen oder andere hart bestrafen. Immunität, Anticrit, Resistances, Sustain, Burst-Fenster und Party-Komposition sind gültige Werkzeuge. **Keine globale Garantie**, dass jede finale Viererparty jeden optionalen Boss besiegt; die Hauptstory-Regel aus §2 bleibt davon unberührt.


## 9.5 Konkrete Encounter-Bausteine — FIXED PHILOSOPHY / EXAMPLE

**Known archetype + unexpected twist:** Ein Gegner darf zunächst wie ein konventioneller schwer gepanzerter Knight aussehen und mit den üblichen Shield-/Physical-Regeln interagieren. Eine zusätzliche Reflect-, Verzögerungs- oder Elementregel kann dieses bekannte Profil bewusst verschieben. Das ist eine Encounter-Idee, keine Pflicht, jedem Boss einen versteckten Twist zu geben.

**Beobachtungs- und Antwortkanäle:** Schadenszahlen (einschließlich 0), tatsächliche Heilung statt Schaden, Reflect, sichtbare Buff-/Debuff-Symbole, wechselnde Resistances, geänderte Turnorder, Ankündigungen, Bewegungen/Animationen und Dialoge können Hinweise liefern. Sie müssen nicht schon vor dem ersten Versuch die komplette Regel verraten, sollten aber nach wiederholtem Beobachten schlüssig sein.

**Beispiele für harte optionale Checks:** Crit-Immunität; stark verminderter Physical Damage; hohe Poison Resistance; eine Lightning-Schwäche; ein zeitlich begrenztes Burst-Fenster; Cleanse-, Sustain- oder Roster-Anforderungen. Früher genannte Zahlen wie 85 % Physical Mitigation sind **illustrative Encounter-Werte**, keine allgemeine globale Balance-Regel.

**Recovery-Grenze:** Seltene überraschende Einzelziel-Todesfälle dürfen den Spieler zum Improvisieren zwingen. Eine häufige rein zufällige komplette Party-Auslöschung ohne Reaktionsfenster ist nicht die beabsichtigte Grundstruktur. Eine spätere konkrete Bossmechanik muss diese Philosophie in einer lernbaren Regel ausdrücken.

## 9.6 Trickster-/Inversion-Design — DIRECTION / OPEN

Gewünscht ist eine besonders ungewöhnliche, Kefka-inspirierte Begegnung: Der Boss kann einen Zustand ankündigen, dessen Wirkung verzögert einsetzt und für eine begrenzte Zahl eigener Züge den üblichen Umgang mit Schaden oder Heilung verändert. Denkbare Zustandsänderungen sind `Physical → Healing`, `Heal → Damage`, `Fire ↔ Ice` oder andere ausdrücklich festgelegte Element-/Effekt-Umkehrungen. `???` oder bewusst unvollständige Anzeigen können das Beobachten fördern.

**Noch nicht entschieden:** Welche Trefferarten invertiert werden; ob Status-Anwendungen, Resistances und Crit mit invertiertem Schaden interagieren; wie Zeitdauer und Tick-Zeitpunkt funktionieren; wie der Zustand angekündigt und beendet wird. Einmal definierte Regeln müssen **intern konsistent** sein. Der alte Wert „10 Turns“ ist eine Beispielzahl, **keine finale Vorgabe**. Die aktuelle freie `Any`-Zielauswahl ist auch deshalb eine bewusst nützliche Grundlage, aber Inversion ist **nicht Teil von Combat-v1**.

## 9.7 World-State und phasenübergreifende Konsequenzen — FIXED / EXAMPLE

Frühere Weltaktionen können einen Boss in einer anderen Form auftreten lassen. Das ursprüngliche GDD-Beispiel: Wenige getötete Castle Guards → der Knight-Boss nutzt Sword/Shield und ist besonders defensiv; viele getötete Guards → Greatsword, offensiverer Kampf und andere physische Mitigation. Das ist ein **Illustrationsfall** und weder ein bestätigter Plotpunkt noch eine feste Killcount-Schwelle.

Auch innerhalb eines Encounters können Entscheidungen/Fehler aus Phase 1 einen späteren Bosszustand oder Phase-3-Angriff beeinflussen. Ein später richtig gelöster Teil muss vorherige Flags nicht automatisch löschen. Gerade optionale Endgame-Bosse dürfen dadurch einen zusammenhängenden Lern- und Planungsprozess statt isolierter Phasen darstellen.

---

# 10. Story, World-State und Exploration

## 10.1 Erzählkern — OPEN

Noch nicht festgelegt sind Hauptfigur/Origin, Auslöser der Reise, Weltkonflikt, Setting, Kapitelstruktur, zentrale Antagonisten und die konkrete Verbindung zwischen Story-Ende und optionalem Endgame. Der Systementwurf darf diese Lücken **nicht mit erfundenem Kanon füllen**.

**DIRECTION:** Anfangs stärker gelenkte Reise, später mehr Offenheit. Background und Kampfklasse können getrennt sein: gemeinsames/classless Intro, später Klassenwahl; unterschiedliche kurze Starts oder ein gemeinsamer Start mit verschiedenem Background sind noch Alternativen. Backgrounds dürfen erzählerisch oder statistisch suboptimale Kombinationen ermöglichen, statt Spieler bei der Herkunftswahl in eine offensichtliche optimale Klasse zu lenken.

## 10.2 Missables und versteckte Bedingungen — FIXED

Unspektakuläre frühe Interaktionen dürfen langfristige Flags setzen und spät Quests, Loot, Charakterbeziehungen, Bosse oder Endgame-Routen öffnen oder schließen. Es muss kein UI-Hinweis sagen „dies ist relevant“. Fortschritts-, Kapitel-, Region- oder Event-Trigger sind als Standardrichtung sinnvoller als ein ständig laufender Echtzeittimer; seltene Zeitbedingungen bleiben möglich, müssten aber ihre Zählweise klar festlegen.

Nicht jeder Run wird jede versteckte Quest, jeden Job-Pfad, jede Waffe und jeden optionalen Boss erreichen. **Verpasster optionaler Content** ist von einem **unbeabsichtigten Story-Softlock** klar zu unterscheiden.


## 10.3 Origin- und Startmodelle — OPEN

Die ausführlichere alte GDD enthielt zwei ernsthaft diskutierte Varianten: **A)** mehrere kurze individuelle Startorte (andere Dörfer/Quartiere), die relativ früh auf eine gemeinsame Hauptstory konvergieren; **B)** ein gemeinsamer Startort, aber unterschiedliche Backgrounds, Familienbeziehungen, Dialoge und eventuell Stats. Alternativ kann ein zunächst klassenloser/shared Intro-Abschnitt zur späteren ersten Jobwahl führen. Keine dieser Varianten ist bereits verbindlicher Story-Kanon.

Herkunft und späterer Combat-Job können voneinander getrennt sein. Ein Background darf ungewöhnliche oder für die gewählte Klasse suboptimale Attribute erzeugen; ein früher illustriertes Beispiel war eine arme, aber belesene Hauptfigur mit zusätzlicher Intelligence, die später Rogue wird. Der Startscreen muss nicht die eine „richtige“ Min-Max-Kombination vorsortieren.

## 10.4 Verborgene Flags, Missables und Trigger — FIXED / DIRECTION

Ein beiläufiges Gespräch mit einem NPC, das rechtzeitige Betreten einer Region oder eine frühere Entscheidung kann ein dauerhaftes Flag setzen. Dieses Flag darf später Quests, Items, Legendary-Komponenten, NPC-Verhalten, alternative Bossformen oder optionale Begegnungen ermöglichen beziehungsweise verhindern. Das UI ist nicht verpflichtet, jede kleine Interaktion als relevant zu kennzeichnen.

Für den Normalfall sind **Kapitel-, Fortschritts-, Event- und Regions-Trigger** als klarere Implementierungsrichtung bevorzugt. Echte Spielzeit kann als seltene Ausnahme relevant sein; dann muss explizit definiert werden, ob Pause, Menüs oder Offline-Zeit zählen. Die früheren Beispiele „NPC vor Abschluss Kapitel 1 angesprochen“ und „Region X vor Boss Y besucht“ sind nur Formbeispiele, keine tatsächlichen Questdaten.

## 10.5 Ausdrücklich nicht entworfener Story-Kanon — OPEN

Noch fehlen Weltname und Karte, der Startzustand der Hauptfigur, die Motivation zum Aufbruch, politische und gesellschaftliche Hintergrundsysteme der Spielwelt, der zentrale Konflikt, konkrete Begleiter-Biografien, Kapitelverlauf, Antagonisten und die erzählerische Brücke zu Superbossen. Das Dokument **bewahrt diese Leerstelle**, statt sie mit automatisch erfundenen Fraktionen, Orten oder Plot-Twists zu füllen. Die Story soll die Systemidentität unterstützen, nicht eine schon jetzt vorgetäuschte vollständige Produktionsplanung sein.

---

# 11. UI und audiovisuelle Präsentation

## 11.1 Erstes Ziel: funktional vor hübsch — FIXED ROADMAP

Die Console ist **nur ein temporärer Testadapter**, nicht das Ziel-UI. Erster Unity-Meilenstein ist ein absichtlich schlichter, aber vollständig steuerbarer Battle-Prototyp: Gegner und HP oben, aktive Party unten, Aktionen/Abilities, Target-Auswahl per Buttons, HP/Mana/Status als einfacher Text oder Balken, Turnanzeige sowie Victory/Defeat. Platzhaltergrafik ist ausdrücklich ausreichend. Animationen, Kamera, Sound, aufwendige Übergänge, VFX und polierte Menüs gehören nicht in den ersten Nachweis.

**OPEN:** endgültiger HUD-Aufbau, Boss-/Normalgegner-Unterschied, Status-Detailtiefe, Tooltip-Layout, Abbildung von Multi-/All-Target, Turnorder-Darstellung und Controller-Bedienung. Als DIRECTION kann ein schlankes Kampf-HUD durch ein optionales Target-Detailfenster ergänzt werden, das Knowledge und `???` für noch unbekannte Informationen berücksichtigt.

## 11.2 Abgrenzung Engine und Combat — FIXED

Unity stellt Eingabe, Visualisierung und Updates bereit. Der Core nimmt **eine vom UI gewählte Aktion und ausgewählte Targets** entgegen, prüft deren fachliche Zulässigkeit und führt sie aus; der Core soll keine `Console.ReadLine()` erwarten oder selbst warten, bis jemand einen Button drückt. Die aktuelle synchrone Menü- und `StartCombat()`-Schleife muss daher in eine **steuerbare Schritt-/Zustandsfolge** übersetzt werden: Turn bereit → Aktion anfordern → UI-Auswahl → Core-Resolution → Ergebnis anzeigen → nächster Turn. Das ist mehr als `Console.WriteLine()` löschen, aber **kein Combat-Neubau**.


## 11.3 Ziel-HUD und Input-Verhalten — DIRECTION / OPEN

Der erste Unity-Proof-of-Concept braucht nur einen verständlichen Combat-Ablauf. Als einfache visuelle Anordnung wurden oben Gegner/HP, unten die aktive Party, ein Action-Menü, HP/Mana-/Statusanzeige, eine Turnorder-Information und eine einfache Victory-/Defeat-Anzeige diskutiert. Die Fähigkeitenauswahl muss die **tatsächlich vorhandenen** Abilities anbieten; Ziele sollen visuell oder notfalls über vorläufige Buttons auswählbar sein. Multi-Target braucht eine erkennbare Auswahl mehrerer verschiedener Ziele; All-Target muss nicht jeden Teilnehmer einzeln anklicken lassen.

Die finale Darstellung von Boss- und Mob-HUD, Controller-Bedienung, Tooltip-Position, Target-Highlight, Buff-/Debuff-Symbolen, Ziel-Detailfenster und `???`-Anzeige ist noch offen. Eine einfache Zusatzansicht für aktuelle Zielinformationen kann später mit dem Knowledge-System verbunden werden. Der schlichte erste Prototyp ist **kein** verpflichtendes visuelles Enddesign.

---
# 12. Technische Architektur und Content-Trennung

## 12.1 Grundsatz — FIXED

**Combat-Core entscheidet, was spielmechanisch passiert.** Unity/Console entscheiden, welche Eingabe angeboten und wie das Ergebnis gezeigt wird. Character-/Ability-/Status-Klassen enthalten Mechanik und Zustand, nicht den Ablauf einer konkreten Konsole. World/Game Data beschreibt Inhalte; spätere Progression definiert Unlock-Quellen; Bestiary/Knowledge ist die Sicht des Spielers auf diese Daten.

Spätere Struktur kann z. B. Combat/TurnOrder, Combat/Targeting, Combat/Damage, Combat/StatusEffects, Characters, Abilities, Items und Presentation/Console umfassen. Die **konkreten** C#-Ordner und Namespaces werden beim Refactor anhand echter Verantwortlichkeiten entschieden. Neue Ordner allein lösen keine Kopplung; keine Architektur um ihrer selbst willen.

## 12.2 Combat-Entscheidung vs. UI-Vorgang — FIXED

- **Regel:** Diese Ability hat ManaCost, RemainingCooldown, TargetType und TargetCount; welche Teilnehmer gültig sind, weiß der Core.
- **Bedienung:** Console druckt Menüpunkte und liest Zahlen; Unity zeigt Buttons und liefert die vom Spieler angeklickte Auswahl.
- **Regel:** Ein nicht ausführbarer Wunsch verbraucht keinen Turn; ein zulässiger, aber unnützer Cast darf ihn verbrauchen.
- **Präsentation:** Zahlen, HP-Bar, Critical-Hit-Text, VFX oder Target-Highlights werden aus tatsächlichen Ereignissen/Resultaten abgeleitet, nicht durch direkte `Console.WriteLine()`-Aufrufe im Core erzeugt.
- **Ablauf:** Nach Abschluss einer gültigen Aktion oder nach Stun startet die Auflösung des nächsten Turns. Die UI wartet zwischen zwei Core-Schritten auf Eingabe; der Core blockiert nicht in einem synchronen Console-Menü.

**DIRECTION:** Ergebnismeldungen/Eventdaten sollen später reichhaltig genug sein, um Crit/Block/Dodge, Buff-Anwendung, Status-Ticks, Heal-Stärke und Endzustände anzeigen zu können. Das bedeutet nicht, jetzt ein großes Event-Framework zu erfinden. Die erste API wird aus dem vorhandenen funktionierenden Kampf abgeleitet.

## 12.3 Zentralisierung und Erweiterbarkeit — FIXED

Balanceformeln und Grundregeln sollen an wenigen geeigneten Stellen liegen. Neue StatusEffects können ihren eigenen eingehenden Schaden über denselben virtuellen Hook verändern, statt dass `Character.DamageTaken()` jeden konkreten Effekt namentlich kennt. Neue Abilities sollen vorhandene Damage-/Target-/Status-Grundlagen verwenden. Neue fundamentale Mechaniken werden **nur** eingeführt, wenn sich der gewünschte Gameplay-Loop mit Bestehendem nicht sinnvoll ausdrücken lässt.

## 12.4 Prototyp, Testinhalt und endgültiger Content — FIXED

Aktuelle Klassenwerte, Skill-Kits, Gegnerlisten, Damage-Multiplikatoren, Test-Encounter und Console-Texte sind **technische Testdaten** und kein finales Balancing. Ein Testskill oder temporärer Debug-Ausdruck wird nach Prüfung wieder entfernt. `main` soll kompilieren und grundlegend spielbar bleiben; vor jedem Push lokal bauen und einen kurzen Smoke-Test machen. Kommentare müssen die **wirklich vorhandene** Logik beschreiben und im selben Schritt wie eine Logikänderung aktualisiert werden.


## 12.5 Langfristige Systemgrenzen — FIXED DIRECTION

**Combat** berechnet Zugberechtigung, zulässige Aktionen/Ziele, Damage/Healing/Status und Ausgang. **Presentation** nimmt Eingaben entgegen und stellt die aus Core-Ergebnissen abgeleiteten Zustände dar. **Content/World Data** definiert, was existiert. **Progression/Unlocks** definiert, woher eine Figur bestimmte Fähigkeiten besitzt. **Knowledge/Bestiary** speichert, welche wahren Informationen der Spieler bereits entdecken durfte. Diese Grenzen machen z. B. eine spätere vollständige Bestiary-Anzeige möglich, ohne die eigentlichen Enemy-Werte zu verändern.

**Breitere Interaktionsvalidierung** (Inventarbesitz, verriegelte Tür, Queststatus, Weltzustand, Ressourcen, Interaktionsreichweite) ist ein möglicher eigener späterer Baustein. Sie wird **nicht** schon wegen der drei jetzigen Combat-Ability-Checks als ein universelles System implementiert. Erst konkrete spätere Interaktionen definieren, welche gemeinsame Abstraktion erforderlich ist.

**Backend-/Datenhaltung:** Ein Datenbank- oder Dateiformat für Charaktere, Items, Progression und World-State ist noch nicht entschieden. Eine C#-Klasse ist nicht automatisch eine künftige SQL-Tabelle; SQL-Lernen oder konzeptionelle Schemata ändern die vereinbarte Reihenfolge `Combat-Cleanup → Console/Core-Split → Unity-Proof-of-Concept` nicht von selbst.

**Projekt-/Codequalität:** Statt alle Regeln in `CombatSystem.cs` zusammenzuziehen, sollen Benutzereingabe, Darstellung und Core-Ablauf bei Bedarf in geeignete Verantwortlichkeiten getrennt werden. Namespace- und Ordnernamen allein erzeugen keine Architektur; jeder strukturelle Schnitt muss durch Verantwortung, Erweiterbarkeit oder Testbarkeit begründet sein. `main` soll kompilieren und grundlegend startbar bleiben.

---

# 13. Aktueller Implementierungsstand (September 2026)

## 13.1 Vorhandener C#-Prototyp — IMPLEMENTED

**Stack:** C# / .NET 10, aktuelle Console-Testoberfläche; Unity als nächste Engine. Der gegenwärtige Prototyp ist ein Combat-Test und **noch keine vollwertige JRPG-Spielwelt**.

- `Character`/`Enemy`, vier Job-Testklassen und Gegnerbeispiele; HP/Mana, Stats, Speed, Crit, Block, Dodge, Defense/Armor, XP-/Level-Grundstruktur.
- Many-vs-Many, dynamische Reihenfolge nach Speed bei einer regulären Aktion pro Runde, Stun- und Cooldown-Verarbeitung.
- Direkter Physical-/Magic-Angriff, `DamageResult`, zentrale direkte-Treffer-`DamageTaken()`-Pipeline und sechs DamageTypes mit Resistances/Weaknesses.
- Ability-Basis mit ManaCost, Cooldown, TargetType und TargetCount; zentrale Auswahl gültiger Ziele, Listen-Execute und Mehrziel-Probelauf.
- Status-Basis, Dauer-/Reapply-Logik, Stun, Slow, Haste, IronGuard, Poison, Bleed und Regeneration.
- Mana-/Cooldown-/No-Targets-Checks vor Ability-Ausführung; Console-Item/Flee sind noch Platzhalter.

Die früher im GDD v0.1 als „nächster großer Schritt“ beschriebenen **Cooldowns, Ability-Targeting und TargetCount** dürfen deshalb in keinem neuen Dokument mehr als nicht implementiert auftauchen.

## 13.2 Vier aktuelle Test-Kits — IMPLEMENTED, NICHT finales Game-Balancing

| Testklasse | Derzeitige vier Fähigkeiten | Testzweck |
|---|---|---|
| Warrior | HeavyStrike, ShieldBash, IronGuard, Hamstring | Physical, Stun/Slow, Defensive und fremdes Buff-Target |
| Rogue | DoubleSlash, PoisonStrike, RendingSlash, Rupture | Multi-Hit, Poison/Bleed, Crit-/Hit-Bedingungen |
| Cleric | SmallRegeneration, MinorHeal, Haste, MinorSmite | HoT/Heal, Support/Speed, Holy Damage |
| Mage | SmallFireball, IceShard, Shock, Meditation | Fire/Ice/Lightning, Slow/Stun-Chance, Self-Mana-Regeneration |

**Hinweis zur IronGuard-UI:** Der Buff wird nach derzeitiger Logik auf das gewählte `Target` gelegt; der bisherige Console-Satz „[User] uses [Name], reducing incoming Physical damage ...“ benennt das Ziel nicht. Beim späteren Reporting-Split muss die Anzeige das tatsächliche Ziel darstellen. Die Fähigkeit ist standardmäßig frei targetbar, sofern ihre Definition nicht ausdrücklich eingeschränkt wird. `Meditation` ist dagegen `Self`.

## 13.3 Aktueller Cleanup-Stand — LOKAL GEMELDET, NICHT NEU GEPUSHT

Nach dem letzten verifizierten `main`-Commit wurde lokal, Datei für Datei und **ohne beabsichtigte Mechanikänderung**, mit dem Hygiene-Cleanup begonnen. Laut bisheriger Rückmeldung bereits bearbeitet: `Program.cs`; Umbenennung `Character/Charakter.cs` → `Character/Character.cs` mit Kommentar-/Spacing-Hygiene; Ability-Basis; StatusEffect-Basis und IronGuardEffect; Haste, Slow, Stun (Dateiname `StunEffect.cs`, **Klasse möglicherweise weiterhin `Stun`**); DoT-/HoT-Basisklassen und kleine Effektdateien.

**Beim nächsten Coding-Einstieg:** Die vier Job-Dateien `Character/Jobs/Rogue/Rogue.cs`, `.../Cleric/Cleric.cs`, `.../Mage/Mage.cs`, `.../Warrior/Warrior.cs` sind als **nächster noch nicht bestätigter Cleanup-Schritt** vorgesehen: unnötige `using`s, alte Skilltree-/Zukunftsdesign-Kommentare entfernen, Constructor-Formatierung vereinheitlichen; Stats, Ability-Reihenfolge und Mechaniken unangetastet lassen. Anschließend die **16 Skill-Dateien** auf Benennung, Beschreibung ↔ tatsächliches Verhalten, alte Sonderlogik und unerwünschte Console-Kopplung prüfen.

**Wichtig:** Der vorliegende Text ist kein Commit und verändert keine Repository-Datei. Bei einem späteren Push lokale Änderungen durch Build und kurze Tests bestätigen. Das Rename einer Datei ist nicht automatisch ein Rename ihrer Klasse.


## 13.4 Verifizierungsgrenzen des letzten dokumentierten Stands — IMPLEMENTED / TO VERIFY

**Code-Beobachtung, kein Testnachweis:** Die zuletzt geprüfte Repository-Fassung (Commit `288fb27e`, 16.09.2026) enthält die direkte Damage-Pipeline in `Character.DamageTaken()`, den virtuellen `StatusEffect.ModifyIncomingDamage()`-Hook, den Physical-Multiplikator `0.7` in `IronGuardEffect` und die Anwendung des Buffs auf das gewählte Ziel. `CombatSystem.PlayerTurn()` enthält den No-valid-target-Check. Die aktuelle `Ability`-Basis besitzt `TargetType` und `TargetCount` und nimmt eine `List<Character>` entgegen.

**Vom Entwickler berichtet:** Build erfolgreich; IronGuard kann einem anderen Charakter gegeben werden; eine temporäre Ability wählte zwei verschiedene Ziele und wirkte auf beide; anschließende Hygiene-Arbeit wurde lokal ausgeführt. Das ist **keine** Aussage, dass ich die lokale Fassung ausgeführt oder sämtliche Combat-Kantenfälle getestet hätte.

**Noch zu prüfen:** numerische 30-%-Reduktion nur bei Physical unter kontrollierten Bedingungen, Status-Refresh im Grenzfall, All-Targets (`TargetCount = 0`) mit echtem Content, Wechselwirkungen von Tod/Start-/End-of-Turn und Regression nach der Console/Core-Trennung. Vollständige Item-, Flee-, Revive- oder Summon-Systeme sind **nicht implementiert**. Die kosmetischen lokalen Dateiumbenennungen aus dem Cleanup sind keine bestätigten neuen Remote-Dateipfade.


## 13.5 Aktuelle 16 Test-Abilities: mechanische Bestandsaufnahme — IMPLEMENTED / TEST DATA

Die folgende Tabelle beschreibt die **auf dem zuletzt verifizierten Remote-Commit sichtbare Testlogik**, nicht die endgültige Job-Balance. Die Werte dienen beim Refactor als Referenz, um versehentliche Verhaltensänderungen zu erkennen. Soweit nicht anders angegeben, nutzen die Skills aktuell den Default `TargetType.Any`, `TargetCount = 1` und wählen intern `Targets[0]`.

| Job / Ability | Aktuelle Kosten / CD | Technisches Testverhalten und Hinweise |
|---|---|---|
| Warrior – **HeavyStrike** | 20 Mana / 3 | NormalAttack; RawDamage auf 150 %; direkter Physical-Treffer. |
| Warrior – **ShieldBash** | 15 / 4 | NormalAttack ×0,8; falls Treffer nicht gedodgt und Ziel lebt: Stun für 1 künftigen eigenen Zielzug. Die Beschreibung erwähnt Shield, der aktuelle Combat prüft aber noch **kein tatsächlich ausgerüstetes Schild**. |
| Warrior – **IronGuard** | 20 / 5 | frei wählbares lebendes Ziel; IronGuardEffect für 3 künftige eigene Zielzüge; im Hook Physical ×0,7; aktueller Console-Text nennt nicht explizit das gewählte Buff-Ziel. |
| Warrior – **Hamstring** | 20 / 5 | NormalAttack ×0,75; nach erfolgreichem überlebten Treffer Slow für 2 zukünftige Zielzüge, Speed −3. |
| Rogue – **DoubleSlash** | 10 / 1 | Maximal zwei getrennte NormalAttacks auf dasselbe Ziel; je Treffer +25 Prozentpunkte auf den Crit-Roll; zweiter Treffer nur, falls Ziel nach dem ersten lebt. |
| Rogue – **PoisonStrike** | 25 / 3 | NormalAttack; nach erfolgreichem überlebten Treffer 50-%-Poison-Chance; Poison 5 Züge, direkter HP-Tick aktuell 2 % Ziel-MaxHP (mindestens 1). |
| Rogue – **RendingSlash** | 25 / 2 | NormalAttack; nach erfolgreichem überlebten Treffer 75-%-Bleed-Chance; Bleed 2 Züge mit Tick von aktuell 50 % des User-PhysicalDamage (mindestens 1). |
| Rogue – **Rupture** | 30 / 5 | NormalAttack; bei bereits aktivem Bleed garantierter Crit-Roll und RawDamage ×2; Refresh des vorhandenen Bleed nur bei erfolgreichem nichttödlichem Treffer. |
| Cleric – **SmallRegeneration** | 15 / 2 | HoT auf gewähltes lebendes Ziel für 4 zukünftige Zielzüge; Tick zu Turnstart aktuell 1 % Ziel-MaxHP (mindestens 1); HP-Cap bleibt aktiv; Ausgabe zeigt nominalen Tick. |
| Cleric – **MinorHeal** | 10 / 1 | Sofortheilung auf gewähltes lebendes Ziel um aktuell 10 % Ziel-MaxHP (mindestens 1), auf MaxHP begrenzt. **Abweichung vom jüngeren Designbeschluss:** die zuletzt gepushte Console-Ausgabe zeigt aktuell `actualHeal`, nicht nominale Heilungsstärke. Beim späteren Reporting-/Skill-Cleanup bewusst angleichen, nicht als bereits erledigt darstellen. |
| Cleric – **Haste** | 30 / 3 | Haste auf gewähltes lebendes Ziel für 3 zukünftige Zielzüge; aktuell Speed +10; `OnRemove` nimmt denselben Wert zurück. |
| Cleric – **MinorSmite** | 15 / 0 | Magischer Holy-Treffer ohne Basis-Cooldown; der in `StartCooldown` gesetzte Restwert wird am nächsten eigenen Zug reduziert. |
| Mage – **SmallFireball** | 15 / 2 | Direkter magischer Fire-Treffer; kein automatisch implementierter Burn. |
| Mage – **IceShard** | 10 / 2 | Direkter magischer Ice-Treffer; bei nicht gedodgtem und überlebtem Ziel Slow 2 Züge, Speed −3. |
| Mage – **Shock** | 15 / 2 | Direkter magischer Lightning-Treffer; nach nicht gedodgtem und überlebtem Ziel 30-%-Chance auf Stun 1 Zug. |
| Mage – **Meditation** | 0 / 5 | `TargetType.Self`; stellt derzeit höchstens 50 fehlendes Mana wieder her. Bei **vollem Mana** wird die Aktion im Console-Turn trotzdem beendet, aber die gegenwärtige `Execute`-Verzweigung startet **keinen Cooldown**. Ob auch ein leerer Cast den Cooldown auslösen soll, ist eine bewusst noch zu prüfende Detailregel und **kein** Grund, eine Full-Mana-Aktion ungültig zu machen. |

**Reporting-Regel:** Nominale Heilstärke ist das jüngere beabsichtigte Game-Design, auch wenn `MinorHeal` aktuell noch effektive Heilung meldet. Der spätere UI-Split darf Meldungen und echte HP-Änderungen nicht vermengen. **Testdaten-Regel:** Aufgelistete Mana-Kosten, Chancen, Prozentwerte und CDs sind die aktuelle Baseline der Test-Kits, keine finale Balancing-Zusage für die spätere Klasse gleichen Namens.

---

# 14. Verbindliche Entwicklungsgrenze: Combat-v1 vor Unity

## 14.1 Was bereits abgeschlossen ist

- Enemy-Speed und dynamische Turnorder.
- Statusdauer mit Turn-Start-Snapshot.
- Allgemeines TargetType/TargetCount-System und Auswahl verschiedener Mehrfachziele.
- Allgemeiner Hook zur Veränderung eingehenden Schadens, inklusive IronGuard-Übertragung auf ein anderes Target.
- Minimale Action-Validation für vorhandene Abilities (Mana, Cooldown, mindestens ein gültiges Ziel, kein verlorener Turn bei technisch unmöglicher Aktion).

**Offene Verifikation:** numerischer IronGuard-Smoke-Test und gründlicherer Gesamt-Regressionstest stehen noch aus. Für die aktuell voranschreitende Hygiene-Arbeit muss nicht extra ein neuer Testskill gebaut werden.

## 14.2 Die nächsten Schritte in Reihenfolge — FIXED ROADMAP

1. **Cleanup, Schritt für Schritt:** kosmetische Hygiene; danach Skills auf Verhalten/Beschreibung prüfen; dann `CombatSystem.cs` auf Verantwortlichkeiten, Naming, ungenutzte Reste und Edge Cases untersuchen. Kein neues Feature. Die nominale HoT-Anzeige **nicht** als Bug ändern; `MinorHeal` meldet im letzten verifizierten Code dagegen noch den effektiven Heal und soll bei der Reporting-Bereinigung an die nominale Regel angepasst werden; die direkte DoT-HP-Regel **nicht** ungefragt durch die Mitigation-Pipeline ersetzen.
2. **Console/Core-Split:** Eingabe-/Menü-/Target-Auswahl und unmittelbare Ausgabe aus dem eigentlichen Combat-Core ziehen; `PlayerTurn()` und den synchronen `StartCombat()`-Ablauf in eine von außen steuerbare Zug- und Action-Resolution überführen. Bei jedem Logikumbau gleichzeitig betroffene Kommentare aktualisieren.
3. **Persönlicher Verständnischeck:** Ohne vorheriges Auswendiglernen erklären, bestehenden Code lesen, kleine Fehler finden, eine Mini-Erweiterung selbst formulieren und eine geänderte Anforderung ohne Vorlage bearbeiten. Ziel ist eine präzise Lernlandkarte, keine Schulnote. Syntaxlücken von Verständnislücken unterscheiden und gezielt üben.
4. **Technischer Combat-v1-Abnahmetest:** normaler Angriff, elementarer Schaden/Resistance, Crit/Dodge/Block, IronGuard Physical vs. non-Physical, Stun/Slow/Haste, Poison/Bleed/HoT, Dauer, Reapply, Cooldowns, Ein-/Mehr-/All-Target, Self/Ally/Enemy/DeadAlly soweit real vorhanden, kein gültiges Ziel, Toten-/Win-/Loss-Verhalten, dynamische Speed-Reihenfolge. Keine Dummy-Features allein für einen hübschen Testplan erfinden.
5. **Unity-Proof-of-Concept:** technisch hässlicher, spielbarer Kampf mit Buttons, Zielauswahl und verständlicher Ergebnisanzeige; Platzhalter statt Art-Pipeline.

**Nach dieser Grenze STOP für neue Combat-Fundamentalsysteme bis zum ersten Unity-Kampf**, außer bei echtem Bug oder technischem Blocker. Neue Statusarten, Summon-Architekturen, Fraktionen, neue Endjobs, allgemeine Item-/Quest-Validation, Parry, extra Turn-Systeme, große KI oder komplexe HUD-Sonderfälle gehören zunächst ins Backlog. Der Langzeitumfang des Spiels bleibt groß; **der nächste Lern- und Implementierungsschritt bleibt klein**.

## 14.3 Danach erster vertikaler Ausschnitt — DIRECTION

Nach dem ersten Unity-Combat-Prototyp können Town/Outdoor-Map, Bewegung, Encounter-Übergang, Rückkehr auf die Karte, XP/Level, Loot, Inventar, Shop/Chest und einfache NPC-/Dialog-Struktur schrittweise folgen. Save/Load, Questketten, Story, große Klassenbäume, Art und Audio sind eigene spätere Milestones. Keine Pflicht, alles gleichzeitig umzusetzen.


## 14.4 Technische Abnahme: Was muss vor dem Unity-Umstieg beobachtbar funktionieren? — FIXED ROADMAP

Die finale Combat-v1-Abnahme soll den bestehenden Featureumfang und die nach der Trennung neu entstandene Steuerbarkeit belegen: (a) Basic Attack und vier vorhandene Job-Kits; (b) Mana-/Cooldown-Gating, ungültige Targets ohne Turnverbrauch, aber erlaubte suboptimale Casts; (c) Single-/Multi-/All-Target, soweit echte vorhandene Abilities sie abdecken; (d) Physical/Magic und Resistances, Crit/Dodge/Block und IronGuard gegen Physical/non-Physical; (e) Start-/End-of-Turn, Duration, Stun, Slow/Haste, DoT/HoT, Refresh; (f) dynamische Reihenfolge, tote/spawnende Teilnehmer, Victory/Defeat; (g) Core kann aus externer UI schrittweise gesteuert werden, ohne blockierende Console-Eingabe.

Es wird **kein zusätzlicher Testskill nur für ein vollständiges Kästchen** erfunden, wenn eine Regel in Combat-v1 lediglich vorbereitet, aber noch ohne echten Content vorhanden ist (`DeadAlly`/Revive, umfassende Items, komplexer All-Target-Skill). Stattdessen wird transparent notiert, was nur code-reviewt und was tatsächlich gespielt wurde. Die finale Abnahme umfasst auch Kommentare/Beschreibungen, die nach Logikänderungen dieselbe Semantik ausdrücken müssen.

## 14.5 Nach dem Kampf-Proof-of-Concept: Vertical Slice — DIRECTION

Möglicher späterer geschlossener Ablauf: kleine Town → Outdoor-Map → Bewegung → Encounter → Combat Scene → Party-Aktionen/Status/Turnorder → Victory/Defeat → Rückkehr auf die Map. Danach in separaten Schritten XP/Level, Loot, Inventory, Shop/Chest, einfache NPCs und Dialoge. Welt-Quests, Savegames, vollständige Jobtrees, Musik, Animationen und Art-Produktion sind **eigene** Meilensteine. 2D und Singleplayer bleiben die Scope-Richtung; 3D und Multiplayer sind aktuell keine Entwicklungsziele.

---

# 15. Offene Fragen und bewusst geparkte Ideen

## 15.1 Wichtige offene Entscheidungen

- Exakte Verzweigung **welche Starterklasse → welche zwei Zwischenklassen → welche vier Endjobs**; genaue Klassennamen auf Starter-/Zwischenstufe.
- Mechanische Identität und Abgrenzung vor allem von Gladiator, Ranger, Samurai, Monk, Reaper, Sorcerer, Summoner und Ghostblade; exakte Klassenwaffen/-rüstung.
- Ob frühe Klassenstufen als freiwillige eigenständige Endgame-Pfade zusätzlich zum 16er-Endjob-Gerüst tragfähig sind.
- Konkrete Progressionslevel; Talentpunkt-Häufigkeit; sechs Ability-Slots vs. anderer Wert; genaue Respec-Regeln und Ausnahmen.
- Vier oder fünf aktive Party-Mitglieder; verbindliche Rostergröße; besondere Companion-Varianten.
- Ob Magic generell blockbar bleibt; Reihenfolge/Stacking zukünftiger Mitigation-Effekte; Sonderfall Extra-Turns.
- Genaue Item-/Loot-/Legendary-Zahlen, Economy/Crafting, Gear-Slots und optionale Sockets.
- Start/Origin, Welt, Story, Hauptkonflikt, Regionen, Questdesign, Encounter-Verteilung.
- Bestiary-Unlock-Formeln, finales Kampf-HUD, Target-Detailfenster, Controller-UX.

## 15.2 Backlog, nicht Combat-v1

Fencer/Parry, alternative Ninja-/Berserker-/Hunter-/Beastmaster-/Tamer-Entwürfe, Summon-Loop, Extra-Turns, Team-/Faction-System, Zombie- und Trickster-Inversion, große Enemy-AI, ausgefeilte Item-/Quest-/World-Interaction-Validation, Talent-/Mastery-Netz, vollständiges Equipment/Inventar, Legendary-Quests, World-State-Flags, Bestiary-UI, Save/Load und polierte audiovisuelle Präsentation.

Diese Ideen sind nicht verworfen, aber eine spätere Designentscheidung muss festhalten, ob sie zum finalen Gerüst passen. **Kein 17. Endjob durch ein altes Konzeptdokument, kein neues Fundamentalsystem allein wegen einer coolen Idee.**


## 15.3 Historisches Klassen-/Mechanikarchiv: erhalten, aber nicht stillschweigend aktiv — ARCHIVE / BACKLOG

Die GDD v0.1 enthält ausführliche Konzepte, die **vor** der späteren 4→8→16-Zielstruktur entwickelt wurden. Ihr Verbleib hier bedeutet *Idee bewahren*, nicht *neuen 17. Endjob oder zusätzliche Zwischenklasse beschließen*:

| Früherer Entwurf | Frühere differenzierte Idee | Aktueller Status |
|---|---|---|
| **Ninja** | verwandt mit Assassin, aber mehr Debuffs, Status-/Tempo-/Control- und Gruppenutility statt primär maximalem Single-Target-Burst; Dagger-Überschneidung möglich | Nicht in der derzeitigen 16er-Namensliste; Wiederaufnahme/Umbenennung offen |
| **Berserker** | Plate trotz erhöhter Verwundbarkeit durch spezielle Rüstungseffekte; schwerer Axt-/Heavy-Weapon-Stil, Lifesteal über selbst verursachten Schaden, riskante Offensive; als Ausnahme Dual-Wield mit zwei Einhandwaffen denkbar | Eigenständiges altes Rollenprofil, derzeit kein zusätzlicher Endjob |
| **Warrior-/Vanguard-artiger Off-Tank** | stabilerer Heavy-Fighter als Berserker, konstante Frontline, schwere Waffen | Arbeitskonzept ohne festgelegten Endjob-Slot |
| **Fencer** | leichte Rüstung, Rapier/Einhandwaffe, Präzisionsdefensive; Parry → Riposte/Counter-Loop | Eher mögliche Special Class; weder Klassenbaum noch Parry beschlossen |
| **Parry als Core-System** | Treffer vermeiden/reduzieren, Counter-Zustand erzeugen, besondere Riposte-Ability öffnen | Nur bei erwiesenem eigenständigem Gameplay-Nutzen; nicht Combat-v1 |
| **Hunter / Beastmaster / Tamer** | frühere Fernkampf-/Begleiter-Archetypen und möglicher eigener Pfad | Ranger ist aktueller Endjob-Name; Hunter-/Tamer-Konzept nicht automatisch dessen Loop |
| **Monk-Unterpfade** | offensive Battle-Monk/Kung-Fu-artige Richtung oder defensiv-spirituelle Identität | Monk bleibt aktueller Endjob; genaue Fähigkeit/Unterpfad offen |
| **offensiver Holy-/Dark-Caster** | zusätzliche Rolle neben Priest/Cleric/Chanter | `Reaper` ist jüngerer Name im vierten Support-/Healing-Slot; eine 1:1-Gleichsetzung mit dem alten Holy/Dark-Caster ist **nicht** entschieden |
| **vierter Knight-/Heavy-Pfad** | ältere, noch nicht benannte zusätzliche Knight-Identität | `Gladiator` ist nun ein vorgesehener Name; sein Mechanikprofil bleibt offen |

Weitere ältere Beispiele: ein Templar-Gegner mit unerwartetem Reflect, Klassensignaturen wie Assassin-Legendary oder Chanter-Combat-Staff, eine Castle-Guard-Weltflag und eine zeitweilige Trickster-Inversion. Sie sind in den jeweiligen Hauptabschnitten dieser GDD als **Beispiel/Direction** erhalten und werden nicht als konkrete fertig geschriebene Spielwelt ausgegeben.

## 15.4 Erweiterte offene Designentscheidungen — OPEN

**Klassen & Progression:** genaue Starter-/Zwischenjob-Namen; exakte Jobpfade; Levelschwellen; frühe Klassen als Mastery-Endpfade; Talent-/Skill-Netz-Topologie; Talentpunkt-Frequenz; Slot-Limit; Respec-Aufwand und Story-Ausnahmen; Use-by-Mastery; exakte Ability-Unlock-Quellen; companion-exklusive Jobs.

**Combat & Charaktere:** vollständige Faction-/Team-Regeln; spezielle Beschwörungen/Spawns; Extra-Turns und ihr Duration-/Cooldown-Timing; Element-Sonderfälle; magische Blockbarkeit; globale/individuelle Damage-Caps und Status-Immunitäten; Parry; finaler Schadens-/Heal-Report; zulässige NPC-/Boss-Aktionen; Ressourcensysteme über Mana hinaus.

**Gear & Loot:** Waffen-/Armor-/Accessory-Slotstruktur; genaue Klasse↔Waffentyp-Matrix; Gear-Bindungen; Legendary-Drops/Quests/Anzahl; Sockets/Manastones; Crafting, Shops, Economy und begrenzte einzigartige Items.

**Knowledge, World und UX:** Formeln für Bestiary-Unlocks; Boss-vs.-Normalgegner-HUD; konkrete Target-Detailansicht; UI/Controller-Belegung; Regionen/Map und Encounters; Story/Origin/Background; tatsächliche Quest- und Weltflags; Zeitsystem; Save/Load-Struktur; Sound/Art-Style und finale Engine-UI.

---

# 16. Design-Guardrails für jede neue Idee

1. Stärkt sie die Identität einer Klasse oder macht sie alle Jobs ähnlicher?
2. Erzeugt sie eine echte Entscheidung, und welche Option geht dafür verloren?
3. Kann die gewünschte Interaktion bereits mit Targeting, DamageType, Status, Gear, Turnorder oder bestehenden Ressourcen beschrieben werden?
4. Falls nicht: Ist ein neues Core-System durch einen klaren neuen Gameplay-Loop gerechtfertigt?
5. Können Spieler die zugrunde liegende Regel durch Beobachtung lernen, ohne dass die UI jeden Boss löst?
6. Bleibt jeder **legale Story-Save** abschließbar, auch wenn optionale Bosse Builds ausschließen dürfen?
7. Welche Aussagen sind feste Entscheidung, welche bloß Beispiel oder noch nicht implementiert?
8. Muss die Entscheidung jetzt fallen, oder gehört sie ins Backlog nach dem ersten Unity-Kampf?

---

# 17. Zusammenfassung in einem Absatz

**Ascend** ist ein geplantes klassisches, rundenbasiertes 2D-Singleplayer-RPG mit starken Klassenidentitäten, bedeutungsvollen Spezialisierungskosten, strategischem Party-/Roster-Building, gefährlicher Welt, verstecktem Wissen, missbarem Content und optionalen Encountern, die echte Build-Antworten verlangen. Die Hauptstory soll mit jedem legalen Entwicklungsweg grundsätzlich abschließbar bleiben; optionaler Endgame-Content darf darüber hinaus spezifische Fähigkeiten und Entscheidungen voraussetzen. Aktuell existiert ein C#-Combat-Test mit vier Job-Kits und modularen Damage-, Targeting-, Cooldown- und Status-Grundlagen. **Der nächste Schritt ist nicht ein weiterer Job oder Boss, sondern Cleanup → Console/Core-Trennung → Verständnis-/Regressionstest → erster funktionierender Unity-Kampf.**

---


# 18. Vollständigkeits- und Versionsabgleich

## 18.1 Was aus v0.1 wieder ausführlich erfasst ist

| Alter Themenkomplex (`docs/GAME_DESIGN.md` vom 10.09.2026) | Verbindlicher Ort in dieser Master-GDD |
|---|---|
| Spielidentität, Konsequenzen, 100%-Run | §§ 1–2 |
| Kampfstruktur, Timing, Reapplication, Crit/Dodge/Block, Cooldown | § 3 |
| Elemente und zentrale Damage-Pipeline | §§ 3.8 und 4 |
| Party, Main Character, drei Companion-Typen | § 5 |
| Klassenprogression, Mastery-Constellation, Unlock-Quellen | § 6 |
| Waffen-Tiers, Requirements, Signature-Gear, kein Smart Loot, seltene Drops | § 7 |
| Beobachtungsbasiertes Bestiary inkl. Uniques-vs.-Repeatables | § 8 |
| Boss-Twists, Trickster, DPS-Checks, Phasen-/World-State-Flags | § 9 |
| Origin, Background, Missables, zeitliche Trigger | § 10 |
| HUD, Target-Detailansicht und Unity-Ziel | § 11 |
| Trennung Combat / Knowledge / UI, Balancezentralisierung | § 12 |
| Combat-v1-Grenze, Vertical Slice und späterer Ausbau | §§ 13–14 |
| Früher eigenständige Klassenideen und offene Fragen | § 15 |

## 18.2 Jüngere Entscheidungen, die alte Passagen ablösen

- **Cooldowns sind implementiert** und haben definierte `Cooldown + 1`-Semantik; sie sind nicht mehr „nächster Implementierungsschritt“.
- **Speed ist eigener Turnorder-Wert** und bestimmt nicht die reguläre Zuganzahl; alte Formulierungen wie „Dexterity/Speed“ sind nicht als Gleichsetzung zu lesen.
- **Statusdauer mit Turn-Start-Snapshot** ist implementiert; eine bei diesem Zug neu erzeugte Wirkung verliert nicht direkt ihren ersten zukünftigen Turn.
- **`TargetType.Any` ist die Default-Regel** für normal zugelassene lebende Ziele; `TargetCount = 0` steht für alle gültigen Ziele.
- **DamageType/Resistance und allgemeiner Incoming-Damage-Hook** sind technisch bereits vorhanden; Poison/Bleed dürfen weiterhin bewusst HP direkt abziehen.
- **Nominale Heal-Anzeige** ist explizite Designentscheidung, kein Bug bei geringer fehlender HP.
- **4→8→16** ist das aktuelle Zielmodell; frühere Ninja-/Berserker-/Fencer-/Hunter-Zweigideen sind archivierte Optionen und keine zusätzlich automatisch zugesagten Jobs. `Gladiator`, `Reaper`, `Ranger`, `Samurai` und `Ghostblade` gehören zur jüngeren 16er-Namensplanung, ihre unentschiedenen Details bleiben offen.
- **Combat-v1 wird nach dem Cleanup nicht weiter in die Breite gebaut.** Danach folgen Console/Core-Split, Verständnis-/Regressionstests und ein bewusst schlichter Unity-Kampf.

## 18.3 Quellenautorität und Änderungskontrolle

Der **GDD-Text** dokumentiert Designabsicht; der zuletzt verifizierte Repository-Code belegt den **implementierten Stand**. Ein früherer GDD-Entwurf oder eine plausible Architekturidee belegt keine ausgeführte Funktion. Für widersprüchliche ältere Entwürfe gilt die **jüngere ausdrücklich getroffene Designentscheidung**; ein Detail ohne entsprechende spätere Entscheidung bleibt `DIRECTION`, `OPEN`, `EXAMPLE` oder `ARCHIVE`.

Diese v0.3 ist zur Ablage als `docs/GAME_DESIGN.md` vorgesehen. Die bisherige ausführliche v0.1 sollte **vor einem eventuellen Replace** z. B. unter `docs/archive/GAME_DESIGN_v0.1_2026-09-10.md` erhalten bleiben. Die verdichtete v0.2 kann optional als historische Zwischenfassung abgelegt werden. **Diese Datei wurde lediglich erzeugt; ein GitHub-Push oder ein Update des Remote-Repositories wird damit nicht behauptet.**

---

## Quellen und Versionshinweise

- Ursprüngliches, detaillierteres Designarchiv: [`docs/GAME_DESIGN.md`, v0.1 vom 10.09.2026](https://github.com/Kryphix95/RPGLearning/blob/main/docs/GAME_DESIGN.md). Dort stehen ältere Ideen, Illustrationen und Detaildiskussionen; bei Widerspruch gilt die **jüngere hier explizit dokumentierte Entscheidung**, nicht der ältere Entwurfsstatus.
- Technischer Code-/README-Ausgangspunkt: [Repository `Kryphix95/RPGLearning`](https://github.com/Kryphix95/RPGLearning), [Commit `288fb27e` vom 16.09.2026](https://github.com/Kryphix95/RPGLearning/commit/288fb27e497405b725b7ee2be1f85a1fc0d2cd31). Der lokale Hygiene-Cleanup danach wurde vom Entwickler berichtet, aber nicht durch einen neueren Remote-Commit in diesem Dokument verifiziert.
- Dieses Dokument ist **v0.3 (Master-GDD)**, Stand **23.09.2026**. Es ist als Ersatz für die bisherige `docs/GAME_DESIGN.md` vorbereitet, ersetzt sie aber nicht automatisch im GitHub-Repository und enthält bewusst keine neuen Story- oder Mechanikentscheidungen für bisher offene Punkte.

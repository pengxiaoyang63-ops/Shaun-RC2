path = "Assets/Scenes/SampleScene.unity"
with open(path, encoding="utf-8", newline="") as f:
    lines = f.readlines()

out, conflicts, i = [], [], 0
while i < len(lines):
    if lines[i].startswith("<<<<<<<"):
        ours, theirs = [], []
        i += 1
        while i < len(lines) and not lines[i].startswith("======="):
            ours.append(lines[i]); i += 1
        assert i < len(lines), "unterminated: missing ======="
        i += 1
        while i < len(lines) and not lines[i].startswith(">>>>>>>"):
            theirs.append(lines[i]); i += 1
        assert i < len(lines), "unterminated: missing >>>>>>>"
        i += 1
        conflicts.append((ours, theirs)); out.append(("C", len(conflicts) - 1))
    else:
        out.append(lines[i]); i += 1

print("conflicts found:", len(conflicts))
assert len(conflicts) == 6
for n, (o, t) in enumerate(conflicts, 1):
    print("  C%d: ours=%4d lines  theirs=%4d lines" % (n, len(o), len(t)))

# Assert the expected shape before trusting it
assert all(len(conflicts[k][0]) == 0 for k in (0, 2, 3, 4, 5)), "unexpected non-empty ours"
assert conflicts[1][1] == ["  reactionDuration: 0.6\n"], conflicts[1][1]
assert conflicts[1][0][0].strip() == "reactionDuration: 6"
assert any("Sample Inventory item" in l for l in conflicts[0][1])
assert any("Sample Inventory item 2" in l for l in conflicts[2][1])
assert any("Sample Respawnable item" in l for l in conflicts[3][1])
assert any("m_Name: WorldState" in l for l in conflicts[4][1])

# C2: keep upstream (337 object lines + reactionDuration: 6). Others: keep stash additions.
choose_ours = {1}
resolved = []
for item in out:
    if isinstance(item, tuple):
        o, t = conflicts[item[1]]
        resolved.extend(o if item[1] in choose_ours else t)
    else:
        resolved.append(item)

assert not any(l.startswith(("<<<<<<<", "=======", ">>>>>>>")) for l in resolved), "markers remain"

with open(path, "w", encoding="utf-8", newline="") as f:
    f.writelines(resolved)
print("resolved ->", len(resolved), "lines")

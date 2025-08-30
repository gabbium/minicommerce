import os

IGNORED_DIRS = {"bin", "obj", ".git", "node_modules", "dist", "packages", ".vs"}
MAX_DEPTH = 99
OUTPUT_FILE = "project-tree.txt"


def tree(dir_path, prefix="", depth=0, max_depth=MAX_DEPTH):
    if depth > max_depth:
        return []

    entries = []
    try:
        items = sorted(os.listdir(dir_path))
    except PermissionError:
        return entries

    for i, item in enumerate(items):
        path = os.path.join(dir_path, item)
        connector = "└── " if i == len(items) - 1 else "├── "
        if os.path.isdir(path):
            if item in IGNORED_DIRS:
                continue
            entries.append(f"{prefix}{connector}{item}/")
            extension = "    " if i == len(items) - 1 else "│   "
            entries.extend(tree(path, prefix + extension, depth + 1, max_depth))
        else:
            entries.append(f"{prefix}{connector}{item}")
    return entries


if __name__ == "__main__":
    root = os.getcwd()  # pega a pasta atual
    lines = [os.path.basename(root) + "/"]
    lines.extend(tree(root))

    with open(OUTPUT_FILE, "w", encoding="utf-8") as f:
        f.write("\n".join(lines))

    print(f"Done! {OUTPUT_FILE}")

# Crotto Plinius — First Launch Guide

## 1. Prerequisites

- **.NET 8.0 SDK** (LTS) or later
  - On macOS (Homebrew): `brew install dotnet-sdk`
  - Ensure `dotnet` is in `$PATH`:
    ```bash
    export PATH="$PATH:/usr/local/share/dotnet:$HOME/.dotnet"
    ```
  - Verify version:
    ```bash
    dotnet --version
    ```

---

## 2. Compilation & Startup

From the project root (`/Users/giona/Documents/code/crotto_plinius`):

```bash
# Restore NuGet dependencies and build
dotnet build

# Start the application
dotnet run
```

For active local development with hot reload:
```bash
dotnet watch run
```

---

## 3. Database Lifecycle & Auto-Provisioning

- **Database Engine:** SQLite (stored at `./crotto_plinius.db`).
- **Initial Boot:** The application executes `DbInitializer.Initialize` inside `Program.cs`.
- **Idempotency Guarantee:**
  - `context.Database.EnsureCreated()` provisions the schema.
  - Initial categories, regional dishes, and default administrative credentials are only seeded if the corresponding tables are empty. Subsequent starts will not overwrite live restaurant data.

---

## 4. Endpoints & Access

| Surface | Path | Description |
| :--- | :--- | :--- |
| **Public Landing** | `https://localhost:5001/` | Hero section, featured daily dishes, structured JSON-LD data |
| **Public QR Menu** | `https://localhost:5001/menu` | Stable URL for table QR codes; fast category navigation |
| **Admin Login** | `https://localhost:5001/admin/login` | Secure cookie authentication entry point |
| **Admin Dashboard** | `https://localhost:5001/admin` | Operational metrics, out-of-stock monitor |
| **Dishes Management** | `https://localhost:5001/admin/dishes` | Price editing, 1-click availability toggles |
| **Categories Management** | `https://localhost:5001/admin/categories` | Section sorting and visibility controls |

### Default Administrative Credentials
- **Username:** `admin`
- **Password:** `CrottoPlinius2026!`

*(Note: Change password in production or seed a dedicated hash using `IPasswordHasher<AdminUser>`)*.

---

## 5. DBeaver Database Connection & Verification

1. Open **DBeaver**.
2. Create New Connection $\rightarrow$ **SQLite**.
3. Path: Point to `/Users/giona/Documents/code/crotto_plinius/crotto_plinius.db`.
4. Run the verification script:

```sql
-- 1. Verify table counts
SELECT 'Categories' AS entity, COUNT(*) AS count FROM Categories
UNION ALL
SELECT 'Dishes' AS entity, COUNT(*) AS count FROM Dishes
UNION ALL
SELECT 'AdminUsers' AS entity, COUNT(*) AS count FROM AdminUsers;

-- 2. Verify relational integrity and price ranges
SELECT 
    c.SortOrder,
    c.Name AS Category,
    COUNT(d.Id) AS DishCount,
    SUM(CASE WHEN d.IsAvailable = 1 THEN 1 ELSE 0 END) AS AvailableCount,
    MIN(d.Price) AS MinPrice,
    MAX(d.Price) AS MaxPrice
FROM Categories c
LEFT JOIN Dishes d ON c.Id = d.MenuCategoryId
GROUP BY c.Id, c.SortOrder, c.Name
ORDER BY c.SortOrder ASC;
```

---

## 6. Cold Backup Procedure

To produce a clean, consistent snapshot without locking write operations:

```bash
sqlite3 crotto_plinius.db ".backup 'crotto_plinius.db.backup'"
```

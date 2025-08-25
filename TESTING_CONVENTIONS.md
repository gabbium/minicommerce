# 🧪 Testing Conventions

This document defines the conventions we adopt for tests in the **MiniCommerce** project.  
The goal is to ensure consistency, readability, and maintainability across the codebase.

---

## 📂 Folder Structure

    tests/
     ├── MiniCommerce.Catalog.Application.UnitTests/
     ├── MiniCommerce.Catalog.Infrastructure.IntegrationTests/
     └── MiniCommerce.Catalog.Web.AcceptanceTests/

-   **UnitTests** → Verify isolated behavior of classes (e.g., handlers, services, value objects).
-   **IntegrationTests** → Validate integration between components (e.g., repositories, database, external APIs).
-   **AcceptanceTests** → Verify the application end-to-end through HTTP, simulating real user behavior.

---

## 📖 Naming Conventions

### 🔹 Unit Tests

-   **Test Class Name** → `[TargetClass]Tests`  
    Example:

    ```csharp
    ListProductsQueryHandlerTests
    ProductDomainServiceTests
    ```

-   **Test Method Name** →

    -   `Action_Result` (for simple cases)
    -   `Action_WhenCondition_ThenResult` (when there is a condition)

    Example:

    ```csharp
    HandleAsync_ReturnsSuccess
    HandleAsync_WhenProductNotFound_ThenReturnsFailure
    ```

-   **Mocks & Test Doubles**
    -   Prefix with `_` and in English.  
        Example:
        ```csharp
        private readonly Mock<IListProductsService> _listProductsServiceMock;
        ```
    -   Shared fakes/helpers go into `TestHelpers/`.

---

### 🔹 Integration Tests

-   **Test Class Name** → `[FeatureOrComponent]Tests`  
    Example:

    ```csharp
    ProductRepositoryTests
    AuthenticationFlowTests
    ```

-   **Test Method Name** → **Scenario-style sentence in PascalCase**  
    Example:

    ```csharp
    ProductIsCreatedAndLoadedCorrectly
    ProductIsUpdatedCorrectly
    ProductIsDeletedCorrectly
    ```

-   **Database Setup**
    -   Use an isolated test database for repeatability.
    -   Use a shared fixture (e.g., with Testcontainers) that starts the database once per test collection.
    -   Reset state between tests (truncate tables, transaction rollback, or tools like Respawn).
    -   Apply fixtures/builders to avoid duplication when creating entities.

---

### 🔹 Acceptance Tests

Acceptance tests follow a **BDD-like structure** with `Feature` and `Steps` classes.

-   **Feature Class Name** → `[Action][Entity]Feature`  
    Example:

    ```csharp
    CreateProductFeature
    UpdateProductFeature
    ListProductsFeature
    GetProductByIdFeature
    ```

-   **Feature Method Name** → **Scenario in PascalCase** (short, descriptive sentence)  
    Example:

    ```csharp
    UserCreatesProductWithValidData
    AnonymousUserAttemptsToCreateProduct
    ForbiddenUserAttemptsToListProducts
    ```

-   **Steps Class Name** → `[Action][Entity]Steps`  
    Example:

    ```csharp
    CreateProductSteps
    UpdateProductSteps
    ListProductsSteps
    ```

-   **Steps Method Name** → `Given...`, `When...`, `Then...`

    -   Keep names explicit and descriptive.
    -   Do **not** use `Should` (e.g., `ThenResponseIs200Ok`, not `ThenTheResponseShouldBe200Ok`).
    -   Prefer explicit parameters (DTOs like `CreateProductEndpoint.Request`) instead of storing state in fields.

    Example:

    ```csharp
    public async Task GivenAnExistingProduct(CreateProductEndpoint.Request request)
    public async Task WhenTheyAttemptToUpdateProduct(Guid id, UpdateProductEndpoint.Request request)
    public async Task ThenResponseContainsProduct(Guid expectedId, UpdateProductEndpoint.Request expected)
    ```

-   **Fixture**
    -   Use a shared `TestFixture` with Testcontainers to manage the HTTP client and database.
    -   `CommonStepsBase` provides helpers like `ReadBodyAs<T>()`, `HttpResponse`, etc.

---

## ✔️ Best Practices

-   **AAA pattern (Arrange, Act, Assert)** for clarity.
-   Keep **one main assert per test** (auxiliary asserts allowed).
-   Prefer **explicit names over short names**.
-   Avoid duplication → use **fixtures, builders, or helpers**.
-   Integration tests may be **slower**: run them in separate pipelines if possible.
-   Acceptance tests should read like **scenarios**: `Given... When... Then...` with explicit data in the steps.

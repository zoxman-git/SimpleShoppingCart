
# RUNNING APP - TROUBLESHOOTING NOTES...

If you run into issues launching website in Visual Studio 2022 (Community)

- May need to install minimum missing node version (I installed NVM and then install node 20.0 and made sure NVM is using it)


# PROCESS

1. Selected Visual Studio 2022 project template (ASP.NET Core with Angular)

2. Ran app and did some troubleshooting around it not starting (node version issue)

3. Setup basic database structure and Entity Framework (check that database created on app start, if missing)

4. Create BusinessLogic project and wire up call to DataAccess repository, call from Web project

5. Work on UI - displaying products, displaying cart, calculating total

6. Manual testing, unit tests, some note taking, clean-up, etc...


# POTENTIAL IMPROVEMENTS IN THE FUTURE... (I AM SURE THERE ARE A BUNCH MORE! ANY SUGGESTIONS...?)

1. Multiple discounts for different volume thresholds

2. Price history for products (general audit history for important data)

3. More sophisticated pricing with different types of discounts (percent/dollar based, non volume), rules for applying discounts

4. Wire up user to cart, add ability to retrieve cart(s)

5. Add logging and error handling (with appropriate messaging for user)

6. Add caching at various layers as appropriate (in memory vs distributed)

7. Inventory management... (handling out of stock items)

8. Use services in angular web app and push API calls to service layer, breaking things apart more

9. Destroy angular subscriptions when components are destroyed, better yet do not even use where we do not need them (use async pipe)

10. Security review... (e.g. do not hardcode password in angular app!)

11. Consider leveraging design patterns (e.g. mediator pattern, with orchestration and manager classes, maybe...)

12. Consider looking into auto generation of angular models from api (and documenting API, swagger or swaggerhub, maybe)

13. Add validation and fail fast

14. Responsiveness on various devices... and improve UI in general

15. Tighten up request/response payloads to only transfer what we need to

16. Performance requirements to test against...?


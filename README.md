# PaySocialCredit

A payment gateway for artists on twitter/facebook/instagram. Sell "the next page" of your work or collections.



## Status

Very rough, just outlining the ideas and strutures. Some vue is auto generated and a few DTOs.

Most of the work rn is going on over in serviceInterface





## Development workflow

Our recommendation during development is to run the `dev` npm script or Gulp task and leave it running in the background:

    $ npm run dev

This initially generates a full development build of your Web App then stays running in the background to process files as they’re changed. This enables the normal dev workflow of running your ASP.NET Web App, saving changes locally which are then reloaded using ServiceStack’s built-in hot reloading. Alternatively hitting `F5` will refresh the page and view the latest changes.

Each change updates the output dev resources so even if you stop the dev task your Web App remains in a working state that’s viewable when running the ASP.NET Web App.


### Watched .NET Core builds

.NET Core projects can also benefit from Live Coding using dotnet watch which performs a “watched build” where it automatically stops, recompiles and restarts your .NET Core App when it detects source file changes. You can start a watched build from the command-line with:

    $ dotnet watch run

### Create a production build

Run the `build` npm script or gulp task to generate a static production build of your App saved to your .NET App's `/wwwroot` folder:

    $ npm run build

This will let you run the production build of your App that's hosted by your .NET App.

### Updating Server TypeScript DTOs

Run the `dtos` npm script or Gulp task to update your server dtos in `/src/dtos.ts`:

    $ npm run dtos

### Deployments

When your App is ready to deploy, run the `publish` npm (or Gulp) script to package your App for deployment:

    $ npm run publish

Which will create a production build of your App which then runs `dotnet publish -c Release` to Publish a Release build of your App in the `/bin/net5/publish` folder which can then copied to remote server or an included in a Docker container to deploy your App.

### Testing

Run the `test` npm script or gulp task to launch the test runner in the interactive watch mode:

    $ npm test

To launch a live testing session that continuously reruns tests when making source code changes, run the `test-watch` npm script instead:

    $ npm run test-watch

To run end-to-end integration tests with [cypress](https://www.cypress.io/):

    $ npm run e2e


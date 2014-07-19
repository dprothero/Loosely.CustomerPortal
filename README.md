A Real-World MassTransit Customer Portal Example
================================================

Now that we’ve seen [some](http://looselycoupledlabs.com/2014/06/masstransit-publish-subscribe-example/) simple [examples](http://looselycoupledlabs.com/2014/07/creating-a-rabbitmq-cluster-for-use-with-masstransit/) of how to use [MassTransit](http://masstransit-project.com/) with the Publish/Subscribe pattern on multiple machines, let’s build something that resembles a more real-world app. In this article, we’ll build an ASP.NET MVC Customer Portal app where a customer can create a new support ticket. The ticket will be published onto the service bus. We’ll create a Windows Service to be the subscriber of these messages and it will handle the tickets, in this example, sending a confirmation email to the customer.

This is a big one, so roll up your sleeves…

[Continue Reading](http://looselycoupledlabs.com/2014/07/a-real-world-masstransit-customer-portal-example/)

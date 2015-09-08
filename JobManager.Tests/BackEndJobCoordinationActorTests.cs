using Akka.Actor;
using Akka.TestKit;
using Akka.TestKit.Xunit;
using JobManager.Actors;
using JobManager.Messages;
using Xunit;

namespace JobManager.Tests {
    public class BackEndJobCoordinationActorTests : TestKit {
        [Fact]
        private void Test1() {
            //TestActorRef<BackEndJobCoordinationActor> backEndJobCoordinationActorRef = ActorOfAsTestActorRef<BackEndJobCoordinationActor>("calculator");

            var backEndJobCoordinationActorRef = ActorOf<BackEndJobCoordinationActor>( () => new BackEndJobCoordinationActor(null));
            backEndJobCoordinationActorRef.Tell(new JobConfigLoadOrUpdateMessage());

            var answer = ExpectMsg<JobConfigLoadOrUpdateMessage>();       //Wait up to 3 seconds for the answer to arrive 
           // Assert.Equal(0, 0);
        }
    }
}

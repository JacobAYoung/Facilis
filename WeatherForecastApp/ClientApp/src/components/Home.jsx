import React, {Component} from 'react';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div>
                <h1>Hi, Thank you for your participation in this small test.</h1>
                <p>This is our single-page application, build with :</p>
                <ul>
                    <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a
                        href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform
                        server-side code
                    </li>
                    <li><a href='https://facebook.github.io/react/'>React</a> for client-side code</li>
                    <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
                </ul>
                <h3>But we have a minor issue with our load time of the weather forecast:</h3>
                <p>To mitigate the issue, we would like to implement a caching layer. Only the first request for the
                    weather forecast impacts the user. Any navigation back to the page should rely on the cached data.
                    The current refresh-rate of the weather service is 15 minutes, starting at every full hour.
                    Therefore, the cache eviction should be scheduled to happen each hour on the minute count 0, 15, 30
                    and 45.</p>
                <ul>
                    <li>Please implement a memory cache to hold the weather forecast and ensure it gets flushed as new
                        data becomes available.
                    </li>
                </ul>
            </div>
        );
    }
}

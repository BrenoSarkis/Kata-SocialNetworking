# Kata-SocialNetworking

<p>Implement a console-based social networking application (similar to Twitter) satisfying the scenarios below.</p>
<h3><a id="user-content-scenarios" class="anchor" aria-hidden="true" href="#scenarios"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Scenarios</h3>
<p><strong>Posting</strong>: Alice can publish messages to a personal timeline</p>
<blockquote>
<p>&gt; Alice -&gt; I love the weather today<br>
&gt; Bob -&gt; Damn! We lost!<br>
&gt; Bob -&gt; Good game though.</p>
</blockquote>
<p><strong>Reading</strong>: Bob can view Alice’s timeline</p>
<blockquote>
<p>&gt; Alice<br>
&gt; I love the weather today (5 minutes ago)<br>
&gt; Bob<br>
&gt; Good game though. (1 minute ago)<br>
&gt; Damn! We lost! (2 minutes ago)</p>
</blockquote>
<p><strong>Following</strong>: Charlie can subscribe to Alice’s and Bob’s timelines, and view an aggregated list of all subscriptions</p>
<blockquote>
<p>&gt; Charlie -&gt; I'm in New York today! Anyone wants to have a coffee?<br>
&gt; Charlie follows Alice<br>
&gt; Charlie wall<br>
&gt; Charlie - I'm in New York today! Anyone wants to have a coffee? (2 seconds ago)<br>
&gt; Alice - I love the weather today (5 minutes ago)</p>
</blockquote>
<blockquote>
<p>&gt; Charlie follows Bob<br>
&gt; Charlie wall<br>
&gt; Charlie - I'm in New York today! Anyone wants to have a coffee? (15 seconds ago)<br>
&gt; Bob - Good game though. (1 minute ago)<br>
&gt; Bob - Damn! We lost! (2 minutes ago)<br>
&gt; Alice - I love the weather today (5 minutes ago)</p>
</blockquote>
<h3><a id="user-content-general-requirements" class="anchor" aria-hidden="true" href="#general-requirements"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>General requirements</h3>
<ul>
<li>Application must use the console for input and output;</li>
<li>User submits commands to the application:
<ul>
<li>posting: &lt;user name&gt; -&gt; &lt;message&gt;</li>
<li>reading: &lt;user name&gt;</li>
<li>following: &lt;user name&gt; follows &lt;another user&gt;</li>
<li>wall: &lt;user name&gt; wall</li>
</ul>
</li>
<li>Don't worry about handling any exceptions or invalid commands. Assume that the user will always type the correct commands. Just focus on the sunny day scenarios.</li>
<li>Use whatever language and frameworks you want. (provide instructions on how to run the application)</li>
<li><strong>NOTE:</strong> "posting:", "reading:", "following:" and "wall:" are not part of the command. All commands start with the user name.</li>
</ul>
</article>
